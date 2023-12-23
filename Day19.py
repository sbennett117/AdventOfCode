def parse_rule(rule: str):
    return (rule[0], rule[1], *rule[2:].split(":")) if ":" in rule else ("", rule)
    

def parse_wf(workflow: str):
    name, rest = workflow.split("{", 1)
    rules = rest[:-1].split(",")
    return (name, [parse_rule(r) for r in rules])

def process_parts(input: str):
    def process_part(wf: str, xmas: dict):
        def resolve_rule(rule_name):
            if rule_name == "A":
                return True
            elif rule_name == "R":
                return False
            else:
                return process_part(rule_name, xmas)
        for rule in wf_map[wf]:
            if len(rule) == 2:
                return resolve_rule(rule[1])
            else:
                score, comp, target, result = rule
                if (xmas[score] < int(target) if comp == "<" else xmas[score] > int(target)):
                    return resolve_rule(result)
    
    workflows, parts = input.split("\n\n", 1)
    wf_map = dict([parse_wf(wf) for wf in workflows.splitlines()])
    parts = [dict([(s[0], int(s[2:])) for s in p[1:-1].split(",")]) for p in parts.splitlines()]
    return sum([sum(p.values()) for p in parts if process_part('in', p)])

input = """px{a<2006:qkq,m>2090:A,rfg}
pv{a>1716:R,A}
lnx{m>1548:A,A}
rfg{s<537:gd,x>2440:R,A}
qs{s>3448:A,lnx}
qkq{x<1416:A,crn}
crn{x>2662:A,R}
in{s<1351:px,qqz}
qqz{s>2770:qs,m<1801:hdj,R}
gd{a>3333:R,R}
hdj{m>838:A,pv}

{x=787,m=2655,a=1222,s=2876}
{x=1679,m=44,a=2067,s=496}
{x=2036,m=264,a=79,s=2244}
{x=2461,m=1339,a=466,s=291}
{x=2127,m=1623,a=2188,s=1013}
"""

with open("inputs/Day19.txt") as r:
    input = r.read()
    print(process_parts(input))