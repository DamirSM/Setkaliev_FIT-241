inf = float('inf')
matrix = [
    [0, 10, 15, 0, 7, 0],
    [10, 0, 8, 0, 7, 0],
    [15, 8, 0, 8, 8, 0],
    [0, 0, 8, 0, 3, 0],
    [7, 7, 8, 3, 0, 20],
    [0, 0, 0, 0, 20, 0]
]

length = len(matrix)
visited = [0]
edges = []
edge_sum = 0
for _ in range(length - 1):
    min_e = inf
    for n in range(length):
        if (n in visited):
            for i in range(length):
                edge = matrix[n][i]
                if i not in visited and edge < min_e and edge != 0:
                    min_e = edge
                    min_i = i
                    parent = n
    if min_e != inf:
        visited.append(min_i)
        edges.append((parent, min_i, min_e))
        edge_sum += min_e
print(edges)
print(edge_sum)