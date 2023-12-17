import math

def find_vertical_reflection(input: str):
    split_input = input.splitlines()
    columns = ["".join([line[i] for line in split_input]) for i in range(len(split_input[0]))]
    for c in range(len(columns)):
        before = columns[c-1::-1]
        after = columns[c:]
        if all([before[i] == after[i] for i in range(min(len(before), len(after)))]):
            return c
    return -1

def find_horizontal_reflection(input: str):
    split_input = input.splitlines()
    for r in range(len(split_input)):
        before = split_input[r-1::-1]
        after = split_input[r:]
        if all([before[i] == after[i] for i in range(min(len(before), len(after)))]):
            return r
    return -1

def find_reflection(input: str):
    vert = find_vertical_reflection(input)
    return vert if vert >= 0 else find_horizontal_reflection(input) * 100

input = """#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#"""
with open("inputs/Day13.txt") as r:
    input = r.read()
    inputs = input.split("\n\n")

    print(sum([find_reflection(i) for i in inputs]))


