from functools import reduce

def hash_word(word: str):
    return reduce(lambda current_number, c: (ord(c) + current_number) * 17 % 256, word, 0)

def sum_hashes(words: list[str]):
    return sum([hash_word(word) for word in words])

def the_procedure(words: list[str]):
    boxes = dict()
    for w in words:
        if w.endswith("-"):
            key = hash_word(w[:-1])
            if key in boxes.keys():
                boxes[key] = [(l, f) for l, f in boxes[key] if l != w[:-1]]
        else:
            key = hash_word(w[:-2])
            if key in boxes.keys():
                old_box = boxes[key]
                matches = [(i, l) for i, l in enumerate(old_box) if l[0] == w[:-2]]
                if len(matches) == 0:
                    boxes[key] = old_box + [tuple(w.split("="))]
                else:
                    boxes[key] = [old_box[i] if i != matches[0][0] else tuple(w.split("=")) for i in range(len(old_box))]
            else:
                boxes[key] = [tuple(w.split("="))]
    return sum([sum([(1 + b) * (i + 1) * int(l[1]) for i, l in enumerate(lenses)]) for b, lenses in boxes.items()])

input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"
with open("inputs/Day15.txt") as r:
    input = r.read()
    words = input.split(",")
    print(sum_hashes(words))
    print(the_procedure(words))
