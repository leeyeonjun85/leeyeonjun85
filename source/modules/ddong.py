import pygame
import random
import os
current_path = os.path.dirname(__file__)

pygame.init() # 초기화(반드시 필요함)

# 화면 크기 설정
screen_width = 480
screen_height = 640
screen = pygame.display.set_mode((screen_width, screen_height))

# 화면 타이틀 설정
pygame.display.set_caption("Pang! Pang!!") # 게임이름

# FPS
clock = pygame.time.Clock()

# 배경 이미지 불러오기
img_path = os.path.join(current_path, "../images/ddong")
background = pygame.image.load(os.path.join(img_path, "background.jpg"))

# 캐릭터 불러오기
character = pygame.image.load(os.path.join(img_path, "character01.png"))
character_30 = pygame.image.load(os.path.join(img_path, "30.png"))
character_size = character_30.get_rect().size
character_width = character_size[0]
character_height = character_size[1]
character_x_pos = screen_width / 2 - character_width / 2
character_y_pos = screen_height - character_height - 20

# 캐릭터 이동좌표
to_x = 0
to_y = 0

# 캐릭터 이동속도
character_speed = 0.3

# 적 캐릭터 기본
enemy = pygame.image.load(os.path.join(img_path, "ddong04.png"))
enemy_size = enemy.get_rect().size
enemy_width = enemy_size[0]
enemy_height = enemy_size[1]
base_speed = 10

# 적1
enemy1_y_pos = 0
enemy1_x_pos = random.randint(0, screen_width - enemy_width )
enemy1_speed = base_speed / 1000 + random.randrange(15, 50) / 200

# 적2
enemy2_y_pos = 0
enemy2_x_pos = random.randint(0, screen_width - enemy_width )
enemy2_speed = base_speed / 1000 + random.randrange(15, 50) / 200

# 적3
enemy3_y_pos = 0
enemy3_x_pos = random.randint(0, screen_width - enemy_width )
enemy3_speed = base_speed / 1000 + random.randrange(15, 50) / 200

# 적4
enemy4_y_pos = 0
enemy4_x_pos = random.randint(0, screen_width - enemy_width )
enemy4_speed = base_speed / 1000 + random.randrange(15, 50) / 200

# 적5
enemy5_y_pos = 0
enemy5_x_pos = random.randint(0, screen_width - enemy_width )
enemy5_speed = base_speed / 1000 + random.randrange(15, 50) / 200

# 적6
enemy6_y_pos = 0
enemy6_x_pos = random.randint(0, screen_width - enemy_width )
enemy6_speed = base_speed / 1000 + random.randrange(15, 50) / 200

# 폰트 정의
game_font = pygame.font.Font(None, 40)

# 게임 시간 정의
total_time = 60 # 플레이 시간
start_ticks = pygame.time.get_ticks() # 시작시간




# 이벤트 루프
running = True
while running:
    dt = clock.tick(60)
    
    # 타이머 계산
    elapsed_time = (pygame.time.get_ticks() - start_ticks) / 1000 # 경과시
    timer = game_font.render(str(int(elapsed_time)), True, (100, 100, 100))
    
    # 시간에 따라 적 속도 증가
    if elapsed_time >= 5:
        base_speed = 50
    if elapsed_time >= 10:
        base_speed = 100
    if elapsed_time >= 15:
        base_speed = 200
    if elapsed_time >= 20:
        base_speed = 250
    
    
    # 키보드 이벤트 처리
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
        
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_LEFT:
                to_x -= character_speed
            elif event.key == pygame.K_RIGHT:
                to_x += character_speed
    
        if event.type == pygame.KEYUP:
            if event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
                to_x = 0
    
    # 캐릭터 위치 처리
    character_x_pos += to_x * dt
    
    # 경계선 처리
    if character_x_pos < 15:
        character_x_pos = 15
    elif character_x_pos > screen_width - character_width - 15:
        character_x_pos = screen_width - character_width - 15
    
    # 적 위치 처리 : 시간에 따라 적 갯수 증가
    if elapsed_time < 6:
        enemy1_y_pos += enemy1_speed * dt
        enemy2_y_pos += enemy2_speed * dt
        enemy3_y_pos += enemy3_speed * dt
    if elapsed_time >= 6:
        enemy1_y_pos += enemy1_speed * dt
        enemy2_y_pos += enemy2_speed * dt
        enemy3_y_pos += enemy3_speed * dt
        enemy4_y_pos += enemy4_speed * dt
    if elapsed_time >= 12:
        enemy5_y_pos += enemy5_speed * dt
    if elapsed_time >= 15:
        enemy6_y_pos += enemy6_speed * dt
    
    if enemy1_y_pos > screen_height:
        enemy1_y_pos = 0
        enemy1_x_pos = random.randint(0, screen_width - enemy_width )
        enemy1_speed = base_speed / 1000 + random.randrange(15, 50) / 200
    if enemy2_y_pos > screen_height:
        enemy2_y_pos = 0
        enemy2_x_pos = random.randint(0, screen_width - enemy_width )
        enemy2_speed = base_speed / 1000 + random.randrange(15, 50) / 200
    if enemy3_y_pos > screen_height:
        enemy3_y_pos = 0
        enemy3_x_pos = random.randint(0, screen_width - enemy_width )
        enemy3_speed = base_speed / 1000 + random.randrange(15, 50) / 200
    if enemy4_y_pos > screen_height:
        enemy4_y_pos = 0
        enemy4_x_pos = random.randint(0, screen_width - enemy_width )
        enemy4_speed = base_speed / 1000 + random.randrange(15, 50) / 200
    if enemy5_y_pos > screen_height:
        enemy5_y_pos = 0
        enemy5_x_pos = random.randint(0, screen_width - enemy_width )
        enemy5_speed = base_speed / 1000 + random.randrange(15, 50) / 200
    if enemy6_y_pos > screen_height:
        enemy6_y_pos = 0
        enemy6_x_pos = random.randint(0, screen_width - enemy_width )
        enemy6_speed = base_speed / 1000 + random.randrange(15, 50) / 200
    
    # 충돌 처리
    character_rect = character_30.get_rect()
    character_rect.left = character_x_pos
    character_rect.top = character_y_pos
    
    
    enemy1_rect = enemy.get_rect()
    enemy2_rect = enemy.get_rect()
    enemy3_rect = enemy.get_rect()
    enemy4_rect = enemy.get_rect()
    enemy5_rect = enemy.get_rect()
    enemy6_rect = enemy.get_rect()
    
    enemy1_rect.left = enemy1_x_pos
    enemy2_rect.left = enemy2_x_pos
    enemy3_rect.left = enemy3_x_pos
    enemy4_rect.left = enemy4_x_pos
    enemy5_rect.left = enemy5_x_pos
    enemy6_rect.left = enemy6_x_pos
    
    enemy1_rect.top = enemy1_y_pos
    enemy2_rect.top = enemy2_y_pos
    enemy3_rect.top = enemy3_y_pos
    enemy4_rect.top = enemy4_y_pos
    enemy5_rect.top = enemy5_y_pos
    enemy6_rect.top = enemy6_y_pos
    
    if character_rect.colliderect(enemy1_rect) | \
    character_rect.colliderect(enemy2_rect) | \
    character_rect.colliderect(enemy3_rect) | \
    character_rect.colliderect(enemy4_rect) | \
    character_rect.colliderect(enemy5_rect) | \
    character_rect.colliderect(enemy6_rect) :
        print('충돌했어요')
        running = False
    
    
    # 배경화면 그리기
    # screen.fill((0, 200, 255))
    screen.blit(background, (0, 0))
    
    # 캐릭터, 적 그리기
    screen.blit(character, (character_x_pos - 20, character_y_pos - 20) )
    screen.blit(enemy, (enemy1_x_pos, enemy1_y_pos) )
    screen.blit(enemy, (enemy2_x_pos, enemy2_y_pos) )
    screen.blit(enemy, (enemy3_x_pos, enemy3_y_pos) )
    screen.blit(enemy, (enemy4_x_pos, enemy4_y_pos) )
    screen.blit(enemy, (enemy5_x_pos, enemy5_y_pos) )
    screen.blit(enemy, (enemy6_x_pos, enemy6_y_pos) )

    
    

    # 타이머 그리기
    screen.blit(timer, (20, 20))
    

    pygame.display.update()

# 잠시 대기
pygame.time.delay(2000)

pygame.quit()
