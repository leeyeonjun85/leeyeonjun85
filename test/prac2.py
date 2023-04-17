# 디버깅연습코드 2. 반복문과 재귀(2)

# Using for loop
print('-----for_loop2 연습 시작-----')
print()
def for_loop2(n):
    x = n
    for i in range(1,n,1):
        x = x*(n-i)
    return x

x = for_loop2(4)
print(x)
print()
print('-----for_loop2 연습 끝-----')

print('\n\n')

# Using Recursion
print('-----recursive2 연습 시작-----')
print()
def recursive2(n):
    if n <= 1:
        return 1

    else:
        return n * recursive2(n-1)

x = recursive2(4)
print(x)
print()
print('-----recursive2 연습 끝-----')