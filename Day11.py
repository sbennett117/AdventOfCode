import math

def count_shortest_paths(stars):
    running_total = 0
    for i in range(len(stars)):
        for j in range(i + 1, len(stars)):
            running_total += abs(stars[i][0] - stars[j][0]) + abs(stars[i][1] - stars[j][1])
    print(running_total)

def expand_input(input: str, times = 2):
    split_input = input.splitlines()
    empty_columns = [i for i in range(len(split_input[0])) if "#" not in [c[i] for c in split_input]]
    empty_rows = [i for i, x in enumerate(split_input) if '#' not in x]

    def calc_x(index):
        corrected = math.floor(index / len(split_input[0]))
        return corrected + len([c for c in empty_rows if c < corrected]) * (times - 1)

    def calc_y(index):
        corrected = index % len(split_input[0])
        return corrected + len([r for r in empty_columns if r < corrected]) * (times - 1)

    return [(calc_x(i), calc_y(i)) for i, x in enumerate("".join(split_input)) if x == "#"]

input = """...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#....."""
with open("inputs/Day11.txt") as r:
    input = r.read()
    count_shortest_paths(expand_input(input, 1000000))
