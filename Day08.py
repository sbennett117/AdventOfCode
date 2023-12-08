import re

def parse(input: str):
    steps, maps = input.split("\n\n")
    map = dict()
    for m in maps.splitlines():
        key, left, right = re.finditer(r"[A-Z0-9]{3}", m)
        map[key.group()] = [left.group(), right.group()]
    
    return (steps, map)

def walk(steps, map):
    current = "AAA"
    i = 0
    while(current != "ZZZ"):
        current = map[current][0] if steps[i % len(steps)] == "L" else map[current][1]
        i = (i + 1)

    print(i)


def ghost_walk(steps: str, map: dict):
    current = [key for key in map.keys() if key[-1] == "A"]
    i = 0
    while(len([c for c in current if c[-1] != "Z"]) > 0):
        for c in range(len(current)):
            current[c] = map[current[c]][0] if steps[i % len(steps)] == "L" else map[current[c]][1]
        i += 1
    
    print(i)

# input = """LLR

# AAA = (BBB, BBB)
# BBB = (AAA, ZZZ)
# ZZZ = (ZZZ, ZZZ)"""
input = """LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)"""

with open("inputs/Day08.txt") as r:
    input = r.read()
    # walk(*parse(input))
    ghost_walk(*parse(input))