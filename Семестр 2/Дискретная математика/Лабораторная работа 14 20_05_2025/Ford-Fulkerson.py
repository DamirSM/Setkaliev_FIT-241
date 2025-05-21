source = 0
sink = 5

matrix = [
    [0, 19, 29, 0, 0, 0],
    [0, 0, 0, 0, 9, 14],
    [0, 0, 0, 31, 15, 0],
    [0, 14, 0, 0, 0, 10],
    [0, 0, 0, 34, 0, 15],
    [0, 0, 0, 0, 0, 0]
]

# matrix = [
#     [0, 76, 47, 0, 0, 41],
#     [0, 0, 0, 0, 44, 56],
#     [0, 0, 0, 25, 15, 0],
#     [0, 35, 0, 0, 13, 29],
#     [0, 0, 0, 0, 0, 50],
#     [0, 0, 0, 0, 0, 0]
# ]

n = len(matrix)
max_flow = 0

while True:
    visited = [source]
    flows = []
    stack = [source]
    edges = []
    while stack:
        selected = stack[-1]
        if selected == sink:
            break
        max_edge = 0
        check = False
        for i in range(n):
            next_vert = matrix[selected][i]
            if i not in visited and next_vert > max_edge:
                vertice = i
                max_edge = next_vert
                check = True
        if not check and len(stack) != 0:
            stack.pop()
        else:
            flows.append(max_edge)
            visited.append(vertice)
            stack.append(vertice)
            edges.append((selected, vertice))
    if sink not in visited:
        break
    reverse_flow = min(flows)
    for edge in edges:
        u,v = edge
        print(u, v)
        matrix[v][u] += reverse_flow
        matrix[u][v] -= reverse_flow
    max_flow += reverse_flow
print(max_flow)