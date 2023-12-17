import math

def expand_line(line, empty_columns, times = 2):
    expanded_line = "".join([line[i]*times if i in empty_columns else line[i] for i in range(len(line))])
    return expanded_line if "#" in expanded_line else expanded_line*times

def expand_input(input, times = 2):
    split_input = input.splitlines()
    empty_columns = [i for i in range(len(split_input[0])) if "#" not in [c[i] for c in split_input]]

    expanded_input = "".join([expand_line(split_input[i], empty_columns, times) for i in range(len(split_input))])
    line_length = len(split_input[0]) + (len(empty_columns) * (times - 1))
    return [(math.floor(i / line_length), i % line_length) for i, x in enumerate(expanded_input) if x == "#"]

def count_shortest_paths(stars):
    running_total = 0
    for i in range(len(stars)):
        for j in range(i + 1, len(stars)):
            running_total += abs(stars[i][0] - stars[j][0]) + abs(stars[i][1] - stars[j][1])
    print(running_total)

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
    # input = r.read()
    count_shortest_paths(expand_input(input))
    count_shortest_paths(expand_input(input, 1000000))