def range_to_map(range_map: list[tuple[str, str, str]]):
    map_dict = dict()
    for dest, source, count in range_map:
        for i in range(int(count)):
            map_dict[int(source) + i] = int(dest) + i
    return map_dict

def run_through_maps(seed, maps: list[dict[int, int]]):
    running_result = maps[0].get(int(seed), int(seed))
    for i in range(1, len(maps)):
        running_result = maps[i].get(running_result, running_result)
    return running_result

def part_1(input: str):
    seeds, *range_maps = input.split("\n\n")
    seeds = seeds.split(": ", 1)[1].split(" ")
    range_maps = [[tuple(r.split(" ", 2)) for r in range_map.splitlines()[1:]] for range_map in range_maps]
    maps = [range_to_map(rm) for rm in range_maps]
    print(min([run_through_maps(seed, maps) for seed in seeds]))

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