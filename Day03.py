from decimal import Decimal
import re
import math

def parse_number(m, length):
    offset = math.floor(Decimal(m.start()) / Decimal(length))
    row = math.floor(Decimal(m.start()-offset) / Decimal(length))
    return [m.group(), row, (m.start()-row) % length, (m.end()-1-row) % length]

def parse_gear(g, length):
    row = math.floor(Decimal(g.start()) / Decimal(length))
    return [row, g.start() % length]

def part_nums(input: str):
    num_of_lines = input.count("\n")
    number_matches = [parse_number(m, num_of_lines) for m in re.finditer(r"[0-9]+", input)]
    symbol_matches = [parse_gear(g, num_of_lines) for g in re.finditer(r"[^0-9\.]", input.replace("\n", ""))]
    numbers = []
    for num, row, start, finish in number_matches:
        if [s for s in symbol_matches if abs(row-s[0]) < 2 and s[1] in range(start-1, finish+2)] != []:
            numbers.append(int(num))
    return sum(numbers)

def gears(input: str):
    num_of_lines = input.count("\n")
    number_matches = [parse_number(m, num_of_lines) for m in re.finditer(r"[0-9]+", input)]
    gear_matches = [parse_gear(g, num_of_lines) for g in re.finditer(r"\*", input.replace("\n", ""))]
    running_total = 0
    for row, column in gear_matches:
        filtered_nums = [n[0] for n in number_matches if abs(n[1]-row) < 2 and column in range(n[2]-1, n[3]+2)]
        if len(filtered_nums) == 2:
            running_total += int(filtered_nums[0]) * int(filtered_nums[1])
    return running_total

# input="""467..114..
# ...*......
# ..35..633.
# ......#...
# 617*......
# .....+.58.
# ..592.....
# ......755.
# ...$.*....
# .664.598.."""
with open("./inputs/Day03.txt") as r:
    input = r.read()
    print(part_nums(input))
    print(gears(input))