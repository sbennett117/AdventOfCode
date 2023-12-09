input = """0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45""".splitlines()

def predict_next_value(line: str, part_2 = False):
    nums = [int(i) for i in line.split(" ")]
    if part_2:
        nums.reverse()
    tree = [nums, [nums[i + 1] - nums[i] for i in range(len(nums) - 1)]]

    while set(tree[-1]) != {0}:
        last_row = tree[-1]
        tree.append([last_row[i + 1] - last_row[i] for i in range(len(last_row) - 1)])
    
    return sum([tree[i-1][-1] for i in range(len(tree) - 1, 0, -1)])

with open("inputs/Day09.txt") as r:
    input = r.readlines()
    print(sum([predict_next_value(line) for line in input]))
    print(sum([predict_next_value(line, True) for line in input]))