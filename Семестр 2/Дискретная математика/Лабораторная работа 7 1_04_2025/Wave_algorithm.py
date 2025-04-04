import queue

matrix = [
    [0, 0, 0, "x", "x"],
    [0, "x", 0, 0, 0],
    [0, "x", 0, "x", 0],
    [0, 0, 0, "x", 0],
    [0, 0, 0, "x", 0]
]

start = [3, 1]
end = [3, 4]

matrix.insert(0,[-1]*len(matrix[0]))
matrix.insert(len(matrix),[-1]*len(matrix[0]))

for i in range(len(matrix)):
    matrix[i].insert(0, -1)
    matrix[i].insert(len(matrix[i]), -1)

for i in range(2):
    start[i] += 1
    end[i] += 1

print(end)

directions = [(0, 1), (0, -1), (1, 0), (-1, 0)]
q = queue.Queue()
x, y = start
q.put((x, y))

while q.qsize != 0:
    x, y = q.get()
    print(x, y)
    for dx, dy in directions:
        nx, ny = x + dx, y + dy
        if (matrix[nx][ny] == -1 or matrix[nx][ny] == "x" or matrix[nx][ny] != 0):
            continue
        matrix[nx][ny] = matrix[x][y] + 1
        q.put((nx, ny))
        if ([nx, ny] == end):
            break
    if ([nx, ny] == end):
        print ("Количество шагов: ", matrix[end[0]][end[1]])
        for i in range(len(matrix)):
            print(matrix[i])
        break
if ([nx, ny] != end):
    print("Невозможно дойти до точки выхода")