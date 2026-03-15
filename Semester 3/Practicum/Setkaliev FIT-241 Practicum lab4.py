epsilon = 1E-14
import numpy as np
import matplotlib.pyplot as plt

def print_no_brackets(X):

    def format_number(x):
        if isinstance(x, int):
            return str(x)
        else:
            if (np.abs(x - int(x)) <= epsilon):
                return str(int(round(x)))
            else:
                return str(round(x,3))

    if X.ndim == 1:
        print(" ".join(format_number(x) for x in X))
    else:
        for row in X:
            print(" ".join(format_number(x) for x in row))

def nf(x):
    return 2 / (np.sin(x) + 4)

print("1.")
a, b = 3, 6

x = np.linspace(0, 10, 100)
y = nf(x)

print_no_brackets(y)


print("\n2.")
plt.plot(x, y, label='Line')
plt.xlabel('x', fontsize=15)
plt.ylabel('y', fontsize=15)
plt.title('Plot', fontsize=25)
plt.legend()
plt.show()

print('\n3.')
plt.scatter(x, y, marker='^', color=(0.5,0.0,0.5), label='Dotted line')
plt.xlabel('x', fontsize=15)
plt.ylabel('y', fontsize=15)
plt.title('Scatter plot', fontsize=25)
plt.legend()
plt.grid(color=(0.0,0.5,0.0), alpha=0.5)
plt.show()

print('\n4.')
size = 100
low = 0
high = 100
centre = (low + high)/2
sigma = (high - low)/6

uni_distr = np.random.randint(low, high+1, size)
print_no_brackets(uni_distr)

normal_distr = np.random.normal(loc=centre, scale=sigma, size=size)
clipped_normal_distr = np.clip(normal_distr, low, high).astype("int")

print_no_brackets(clipped_normal_distr)

plt.hist(uni_distr, bins=10, color=(0.5,0.5,0.0), edgecolor=(0.2,0.2,0.0), range=(0,100))
plt.title('Uniform distribution', fontsize=25)
plt.show()

plt.hist(clipped_normal_distr, bins=20, color=(0.0,0.5,0.5), edgecolor=(0.0,0.2,0.2), range=(0,100))
plt.title('Normal distribution', fontsize=25)
plt.show()


print('\n5.')
size = 50
low = 1
high = 5

uni_distr = np.random.randint(low, high, size)
print_no_brackets(uni_distr)

values, counts = np.unique(uni_distr, return_counts=True)

colors = [(0.8,0.0,0.0), (0.0,0.8,0.0), (0.0,0.0,0.8), (0.8,0.8,0.0)]

def show_numbers(val):
    value = val * size/100
    return f"{value:.0f}"

plt.pie(counts, labels=[str(v) for v in values], colors=colors, autopct=show_numbers)
plt.title("Pie diagram", fontsize=25)
plt.show()

plt.bar(values, counts, color=colors)
plt.xticks(values)
plt.title("Bar diagram", fontsize=25)
plt.xlabel("Values", fontsize=15)
plt.ylabel("Counts", fontsize=15)
plt.show()

print('\n6.')

fun = lambda x, y: (x - 4)**2 + (y - 2)**2

u = np.linspace(0, 10, 100)
v = np.linspace(0, 10, 100)
X, Y = np.meshgrid(u, v)

Z = fun(X, Y)

fig = plt.figure()
ax = fig.add_subplot(projection='3d')

ax.plot_surface(X, Y, Z, color=(0.5,0.0,0.0), edgecolor=(0.2,0.0,0.0), alpha=0.5)

ax.set_title("3D Plot", fontsize=25)
ax.set_xlabel("x", fontsize=15)
ax.set_ylabel("y", fontsize=15)
ax.set_zlabel("z", fontsize=15)

plt.show()

print("\n7.")
fig, axs = plt.subplots(2, 2, figsize=(15, 10))
fig.suptitle("Grid of 4 graphs", fontsize=25)

axs[0,0].plot(x, y, label='Line')
axs[0,0].set_xlabel('x', fontsize=15)
axs[0,0].set_ylabel('y', fontsize=15)
axs[0,0].set_title('Plot', fontsize=25)
axs[0,0].legend()

axs[0,1].scatter(x, y, marker='o', color=(0.5,0.0,0.5), label='Dotted line')
axs[0,1].set_facecolor((0.1,0.1,0.1))
axs[0,1].set_xlabel('x', fontsize=15)
axs[0,1].set_ylabel('y', fontsize=15)
axs[0,1].set_title('Scatter plot', fontsize=25)
axs[0,1].legend()
axs[0,1].grid(color=(0.0,0.5,0.0), alpha=0.5)


axs[1,0].pie(counts, labels=[str(v) for v in values], colors=colors, autopct='%1.1f%%')
axs[1,0].set_title("Pie diagram", fontsize=25)

ax3d = fig.add_subplot(2, 2, 4, projection='3d')
ax3d.plot_surface(X, Y, Z, color=(0.5,0.0,0.0), edgecolor=(0.2,0.0,0.0), alpha=0.5)
ax3d.set_title("3D Plot", fontsize=25)
ax3d.set_xlabel("x", fontsize=15)
ax3d.set_ylabel("y", fontsize=15)
ax3d.set_zlabel("z", fontsize=15)

plt.tight_layout()
plt.show()

print("\n8.")
# print(plt.style.available)

styles = ['Solarize_Light2', 'classic', 'dark_background']

for style in styles:
    plt.style.use(style)
    fig, axs = plt.subplots(2, 2, figsize=(15, 10))
    fig.suptitle("Grid of 4 graphs", fontsize=25)

    axs[0, 0].plot(x, y, label='Line')
    axs[0, 0].set_xlabel('x', fontsize=15)
    axs[0, 0].set_ylabel('y', fontsize=15)
    axs[0, 0].set_title('Plot', fontsize=25)
    axs[0, 0].legend()

    axs[0, 1].scatter(x, y, marker='o', color=(0.5, 0.0, 0.5), label='Dotted line')
    axs[0, 1].set_facecolor((0.1, 0.1, 0.1))
    axs[0, 1].set_xlabel('x', fontsize=15)
    axs[0, 1].set_ylabel('y', fontsize=15)
    axs[0, 1].set_title('Scatter plot', fontsize=25)
    axs[0, 1].legend()
    axs[0, 1].grid(color=(0.0, 0.5, 0.0), alpha=0.5)

    axs[1, 0].pie(counts, labels=[str(v) for v in values], colors=colors, autopct='%1.1f%%')
    axs[1, 0].set_title("Pie diagram", fontsize=25)

    ax3d = fig.add_subplot(2, 2, 4, projection='3d')
    ax3d.plot_surface(X, Y, Z, color=(0.5, 0.0, 0.0), edgecolor=(0.2, 0.0, 0.0), alpha=0.5)
    ax3d.set_title("3D Plot", fontsize=25)
    ax3d.set_xlabel("x", fontsize=15)
    ax3d.set_ylabel("y", fontsize=15)
    ax3d.set_zlabel("z", fontsize=15)

    plt.tight_layout()
    plt.show()
