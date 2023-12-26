def move(x, y, dir):
    if dir is "R":
        return (x, y + 1)
    elif dir is "D":
        return (x + 1, y)
    elif dir is "L":
        return (x, y - 1)
    else:
        return (x - 1, y)

def build_lightmap(input, line_len, line_count, start):
    beams = [start]
    visited = set()
    while len(beams) > 0:
        x, y, dir = beams.pop(0)
        if not(0 <= x < line_len and 0 <= y < line_count):
            continue # out of bounds
        if (x, y, dir) in visited:
            continue # loop
        visited.add((x, y, dir))
        current = input[x * line_len + y % line_len]
        if current == ".":
            beams.append((*move(x, y, dir), dir))
        elif current == "|":
            if dir in "UD": # pass through
                beams.append((*move(x, y, dir), dir))
            else: # split
                beams.append((*move(x, y, "U"), "U"))
                beams.append((*move(x, y, "D"), "D"))
        elif current == "-":
            if dir in "LR": # pass through
                beams.append((*move(x, y, dir), dir))
            else: # split
                beams.append((*move(x, y, "L"), "L"))
                beams.append((*move(x, y, "R"), "R"))
        elif current == "/":
            new_dir = {"U": "R", "R": "U", "L": "D", "D": "L"}[dir]
            beams.append((*move(x, y, new_dir), new_dir))
        else: # must be \
            new_dir = {"U": "L", "L": "U", "R": "D", "D": "R"}[dir]
            beams.append((*move(x, y, new_dir), new_dir))
        
    return len(set([(x, y) for x, y, _ in visited]))

input=r""".|...\....
|.-.\.....
.....|-...
........|.
..........
.........\
..../.\\..
.-.-/..|..
.|....-|.\
..//.|....
"""

with open("inputs/Day16.txt") as r:
    input = r.read()
    line_len = input.find("\n")
    line_count = input.count("\n")
    input = input.replace("\n", "")
    print(build_lightmap(input, line_len, line_count, (0, 0, "R")))