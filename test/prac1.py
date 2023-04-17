# 디버깅연습코드 1. 반복문과 재귀(1)

# Using for loop
print('-----for_loop1 연습 시작-----')
def for_loop1():
    for i in range(5):
        print(f'I have {i} cake(s).')

for_loop1()
print('-----for_loop1 연습 끝-----')

print()

# Using Recursion
print('-----recursive1 연습 시작-----')
def recursive1(i=0):
    if i == 4:
        print(f'I have {i} cake(s).')
        return True 

    print(f'I have {i} cake(s).')
    i+=1 
    return recursive1(i) 

recursive1()
print('-----recursive1 연습 끝-----')

