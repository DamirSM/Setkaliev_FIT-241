# inf = float('inf')
source = 0
sink = 5

# matrix = [
#     [inf, 19, 29, inf, inf, inf],
#     [0, inf, inf, 0, 9, 14],
#     [0, inf, inf, 31, 15, inf],
#     [inf, 14, 0, inf, 0, 10],
#     [inf, 0, 0, 34, inf, 15],
#     [inf, inf, inf, inf, inf, inf]
# ]

matrix = [
    [0, 19, 29, 0, 0, 0],
    [0, 0, 0, 9, 0, 14],
    [0, 0, 0, 31, 15, 0],
    [0, 14, 0, 0, 0, 10],
    [0, 0, 0, 34, 0, 15],
    [0, 0, 0, 0, 0, 0]
]

residual = [row[:] for row in matrix]

max_flow = 0

len = len(matrix)

while True:
    visited = [False] * len
    parent = [-1] * len
    queue = [source]
    visited[source] = True
    while queue:
        u = queue.pop(0)
        for v in range(len):
            if not visited[v] and residual [u][v] > 0:
                queue.append(v)
                visited[v] = True
                parent[v] = u
    if not visited[sink]:
        break
    path_flow = inf
    v = sink
    while v != source:
        u = parent[v]
        path_flow = min(path_flow, residual[u][v])
        v = u
    v = sink
    while v != source:
        u = parent[v]
        residual[u][v] -= path_flow
        residual[v][u] += path_flow
        v = u
    max_flow += path_flow

print(max_flow)