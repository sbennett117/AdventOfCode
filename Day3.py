import re

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
    number_matches = [m for m in re.finditer(r"[0-9]+", input)]
    # split_input = input.splitlines()
    # for i in range(len(split_input)):
    #     for gear in re.findall(r"\*", input[i]):
    #         print(f"({i},{gear.span()})")

with open("./inputs/Day3.txt") as r:
    # input = r.read()
    # print(count_part_numbers(input.splitlines()))
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
    gears(input)