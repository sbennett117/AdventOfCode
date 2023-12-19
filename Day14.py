import math

def north_load_test(input):
    line_len = input.find("\n")
    line_num = input.count("\n")
    input = list(input.replace("\n", ""))

    def calc_x(index):
        return math.floor(index / line_len)

    def calc_y(index):
        return index % line_len

    initial_rocks = [(calc_x(i), calc_y(i)) for i, c in enumerate(input) if c == "O"]
    def move_north(x:int, y:int):
        if x == 0:
            return (x, y)
        input[x * line_len + y] = "."
        for i in range(x, 0, -1):
            if input[(i - 1) * line_len + y] != ".":
                input[i * line_len + y] = "O"
                return (i, y)
        input[y] = "O"
        return (0, y)
    moved_rocks = [move_north(*r) for r in initial_rocks]
    print(sum([line_num - x + 1 for x, _ in moved_rocks]))

input = """O....#....
O.OO#....#
.....##...
OO.#O....O
.O.....O#.
O.#..O.#.#
..O..#O..O
.......O..
#....###..
#OO..#...."""
with open("inputs/Day14.txt") as r:
    input = r.read()
    north_load_test(input)