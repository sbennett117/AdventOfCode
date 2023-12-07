from collections import Counter

FIVE_OF_A_KIND = 6
FOUR_OF_A_KIND = 5
FULL_HOUSE = 4
THREE_OF_A_KIND = 3
TWO_PAIR = 2
ONE_PAIR = 1

def evaluate_hand(hand: str):
    counts = Counter(hand)
    if len(counts.keys()) == 5:
        return 0
    elif len(counts.keys()) == 4:
        return ONE_PAIR
    elif len(counts.keys()) == 3:
        return THREE_OF_A_KIND if 3 in counts.values() else TWO_PAIR
    elif len(counts.keys()) == 2:
        return FOUR_OF_A_KIND if 4 in counts.values() else FULL_HOUSE
    else:
        return FIVE_OF_A_KIND

def evaluate_wildcards(hand: str):
    counts = Counter(hand)
    if (len(counts.keys())) == 5:
        # 2345J becomes 23455
        return ONE_PAIR if "J" in counts.keys() else 0
    elif len(counts.keys()) == 4:
        # 22345, 2234J or JJ345. J will prioritise three of a kind over two pair
        return THREE_OF_A_KIND if "J" in counts.keys() else ONE_PAIR
    elif len(counts.keys()) == 3:
        if 3 in counts.values():
            # Could be JJJ34, 2223J, or 22234. J will prioritise four of a kind
            return FOUR_OF_A_KIND if "J" in counts.keys() else FULL_HOUSE
        elif counts.get("J") == 2:
            # JJ334, which becomes 33334
            return FOUR_OF_A_KIND
        else:
            # Could be 2233J, or 22334
            return FULL_HOUSE if counts.get("J") is not None else TWO_PAIR
    elif len(counts.keys()) == 2:
        if "J" in counts.keys():
            # J2222, JJ222, JJJ22 and JJJJ2 all become 22222
            return FIVE_OF_A_KIND 
        else:
            return FOUR_OF_A_KIND if 4 in counts.values() else FULL_HOUSE
    else:
        return FIVE_OF_A_KIND

def string_to_hand(str, wildcards = False):
    card_values = "J23456789TQKA" if wildcards else "23456789TJQKA"
    hand, bid = str.split(" ")
    evaluated = evaluate_wildcards(hand) if wildcards else evaluate_hand(hand)
    return (hand, bid, evaluated, [card_values.find(c) for c in hand])

input = """32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483""".splitlines()
with open("inputs/Day07.txt") as r:
    input = r.readlines()
    # part one
    hands = sorted([string_to_hand(h) for h in input], key = lambda h: (h[2], *h[3]))
    print(sum((i + 1) * int(hands[i][1]) for i in range(len(hands))))
    # part two
    hands = sorted([string_to_hand(h, True) for h in input], key = lambda h: (h[2], *h[3]))
    print(sum((i + 1) * int(hands[i][1]) for i in range(len(hands))))
