def range_to_map(range_map: list[tuple[str, str, str]]):
    map_dict = dict()
    for dest, source, count in range_map:
        for i in range(int(count)):
            map_dict[int(source) + i] = int(dest) + i
    return map_dict

def part_1(input: str):
    seeds, *range_maps = input.split("\n\n")
    named_ranges = [tuple(r.split(":\n")) for r in range_maps]
    named_ranges = [(n, [tuple(r.split(" ", 2)) for r in range.splitlines()]) for n, range in named_ranges]
    overall_map = dict()
    named_ranges.reverse()
    for name, range in named_ranges:
        temp_map = dict()
        rmap = range_to_map(range)
        for key, value in rmap.items():
            temp_map[key] = overall_map.get(value, value)
        overall_map.update(temp_map)
        # print(f"Finished {name}")
    for seed in seeds.split(": ", 1)[1].split(" "):
        print(overall_map.get(int(seed), seed))

input="""seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4"""

with open("inputs/Day05.txt") as r:
    input = r.read()
    part_1(input)