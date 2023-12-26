
def parse_range_map(range_map: str):
    n, ranges = range_map.split(":\n")
    split_ranges = [tuple(r.split(" ", 2)) for r in ranges.splitlines()]
    r = [(int(source), int(count), int(dest)) for dest, source, count in split_ranges]
    return (n, r)
    
def map_walk(seed, ranges):
    current = int(seed)
    for _, m in ranges:
        for source, count, dest in m:
            if source <= current and source + count > current:
                current = dest + abs(source - current)
                break
    return current

def part_1(input: str):
    seeds, *range_maps = input.split("\n\n")
    named_ranges = [parse_range_map(r) for r in range_maps]

    print(min([map_walk(seed, named_ranges) for seed in seeds.split(": ", 1)[1].split(" ")]))

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
