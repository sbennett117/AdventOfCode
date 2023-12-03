import re
import math

def count_part_numbers(input):
    symbols = set(sum([re.findall(r"[^0-9\.]", x) for x in input], []))
    running_total = 0
    for i in range(len(input)):
        for match in re.finditer(r"[0-9]+", input[i]):
            number = input[i][match.start():match.end()]
            char_before = "" if match.start() == 0 else input[i][match.start()-1]
            char_after = "" if match.end() == len(input[i]) else input[i][match.end()]
            row_above = "" if i == 0 else input[i-1][match.start() if char_before == "" else match.start()-1:match.end() if char_after == "" else match.end()+1]
            row_below = "" if i == len(input)-1 else input[i+1][match.start() if char_before == "" else match.start()-1:match.end() if char_after == "" else match.end()+1]
            if any(x in char_before+char_after+row_above+row_below for x in symbols):
                running_total += int(number)
    return running_total

def gears(input: str):
    split_input = input.splitlines()
    sanitised_input = input.replace("\n", "")
    number_matches = [[m.group(), math.floor(m.start() / len(split_input)), m.start() % len(split_input), m.end() % len(split_input)] for m in re.finditer(r"[0-9]+", sanitised_input)]
    gear_matches = [[math.floor(m.start() / len(split_input)), m.start() % len(split_input)] for m in re.finditer(r"\*", sanitised_input)]
    running_total = 0
    for row, column in gear_matches:
        filtered_nums = [n[0] for n in number_matches if abs(n[1]-row) < 2 and column in range(n[2]-1, n[3]+1)]
        if len(filtered_nums) == 2:
            running_total += int(filtered_nums[0]) * int(filtered_nums[1])
    print(running_total)

input="""467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."""
with open("./inputs/Day3.txt") as r:
    # input = r.read()
    # print(count_part_numbers(input.splitlines()))
    gears(input)