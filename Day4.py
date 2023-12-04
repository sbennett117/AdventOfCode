input = """Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
""".splitlines()

def chunk(str):
    return [int(str[i:i+2]) for i in range(0, len(str), 3)]

def part_1(input):
    total_score = 0
    for card in input:
        _, numbers = card.split(": ")
        winning, supplied = [chunk(n) for n in numbers.split(" | ")]
        score = 0
        for n in supplied:
            if n in winning:
                score = 1 if score == 0 else score * 2
        total_score += score
    print(total_score)

def part_2(input):
    numbers = [card.split(": ", 1)[1].split(" | ") for card in input]
    cards = [(chunk(winning), chunk(supplied), 1) for winning, supplied in numbers]
    for i in range(len(cards)):
        winning, supplied, number_of = cards[i]
        score = 0
        for n in supplied:
            if n in winning:
                score += 1
                win, sup, num = cards[i+score]
                cards[i+score] = (win, sup, num + number_of)
    print(sum([n[2] for n in cards]))

with open('./inputs/Day4.txt') as r:
    input = r.readlines()
    part_1(input)
    part_2(input)