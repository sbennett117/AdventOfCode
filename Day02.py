# input = """Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
# Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
# Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
# Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
# Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"""

def count_games(input):
    game_map = {}
    for i in input:
        game_number, counts = i.strip().split(": ", 1)
        max_counts = {}
        for count in counts.split("; "):
            colours = count.split(", ")
            colourCounts = {}
            for colourPair in colours:
                number, colour = colourPair.split(" ")
                colourCounts[colour] = colourCounts.get(colour, 0) + int(number)
            for colour, number in colourCounts.items():
                max_counts[colour] = max(max_counts.get(colour, 0), number)
        game_map[int(game_number.split(" ")[1])] = max_counts
    return game_map

def check_legality(games):
    running_total = 0
    for game, max_counts in games.items():
        if max_counts.get("red", 0) > 12:
            pass
        elif max_counts.get("green", 0) > 13:
            pass
        elif max_counts.get("blue", 0) > 14:
            pass
        else:
            running_total += game
    return running_total

def find_powers(games):
    powers = [count["red"] * count["green"] * count["blue"] for count in games.values()]
    return sum(powers)

with open("./inputs/Day02.txt") as r:
    print(check_legality(count_games(r.readlines())))
    print(find_powers(count_games(r.readlines())))