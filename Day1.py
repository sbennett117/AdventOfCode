import regex

numberDict = {"one": "1", "two": "2", "three": "3", "four": "4", "five": "5", "six": "6", "seven": "7", "eight": "8", "nine": "9"}

def part1():
    def transform(str):
        stripped = regex.findall(r"[0-9]", str)
        return int(stripped[0] + stripped[-1])

    with open("./inputs/Day1.txt") as r:
        input = r.read().splitlines()
        input = map(transform, input)
        print(sum(input))

def part2():
    def replace(str):
        if str in numberDict.keys():
            return numberDict[str]
        return str

    def transform(str):
        stripped = regex.findall(r"[0-9]|one|two|three|four|five|six|seven|eight|nine", str, overlapped=True)
        translated = list(map(replace, stripped))
        return int(translated[0] + translated[-1])

    with open("./inputs/Day1.txt") as r:
        input = r.read().splitlines()
        input = map(transform, input)
        print(sum(input))

part1()
part2()