import re

input = """Time:      7  15   30
Distance:  9  40  200""".splitlines()

def part_one(input):
    times, distances = [re.findall(r"[0-9]+", i) for i in input]
    running_total = 1
    for i in range(len(times)):
        running_total *= (len([j for j in range(int(times[i]) + 1) if j * (int(times[i]) - j) > int(distances[i])]))
    print(running_total)

with open("inputs/Day06.txt") as r:
    input = r.readlines()
    part_one(input)
    part_one([i.replace(" ", "") for i in input])