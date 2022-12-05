import pygame
import os
import sys



def resource_path(relative_path):
    try:
        base_path = sys._MEIPASS
    except Exception:
        base_path = os.path.abspath(".")
    return os.path.join(base_path, relative_path)



pygame.init() # 초기화(반드시 필요함)

# 화면 크기 설정
screen_width = 640
screen_height = 480
screen = pygame.display.set_mode((screen_width, screen_height))

# 화면 타이틀 설정
pygame.display.set_caption("Pang!!") # 게임이름

# FPS
clock = pygame.time.Clock()

# 이미지 불러오기
# current_path = os.path.dirname(__file__)
# img_path = os.path.join(current_path, "../images/pang")

# background = pygame.image.load(os.path.join(img_path, "background.jpg")) # 배경
background = pygame.image.load("D:/coding/games/pang/background.jpg") # 배경

# stage = pygame.image.load(os.path.join(img_path, "stage.png")) # 스테이지
stage = pygame.image.load("D:/coding/games/pang/stage.png") # 스테이지
stage_size = stage.get_rect().size
stage_height = stage_size[1]

# character = pygame.image.load(os.path.join(img_path, "character.png")) # 캐릭터
character = pygame.image.load("D:/coding/games/pang/character.png") # 캐릭터
# character1 = pygame.image.load(os.path.join(img_path, "character1.png")) # 캐릭터
character1 = pygame.image.load("D:/coding/games/pang/character1.png") # 캐릭터
character_size = character.get_rect().size
character_width = character_size[0]
character_height = character_size[1]
character_x_pos = (screen_width / 2) - (character_width / 2)
character_y_pos = screen_height - character_height - stage_height


# 캐릭터 이동좌표
character_to_x = 0

# 캐릭터 이동속도
character_speed = 5

# 무기
# weapon = pygame.image.load(os.path.join(img_path, "weapon.png")) # 무기
weapon = pygame.image.load("D:/coding/games/pang/weapon.png") # 무기
weapon_size = weapon.get_rect().size
weapon_width = weapon_size[0]
weapons = []
weapon_speed = 10

# 공
# ball_images = [
#     pygame.image.load(os.path.join(img_path, "ball1.png")),
#     pygame.image.load(os.path.join(img_path, "ball2.png")),
#     pygame.image.load(os.path.join(img_path, "ball3.png")),
#     pygame.image.load(os.path.join(img_path, "ball4.png"))]
ball_images = [
    pygame.image.load("D:/coding/games/pang/ball1.png"),
    pygame.image.load("D:/coding/games/pang/ball2.png"),
    pygame.image.load("D:/coding/games/pang/ball3.png"),
    pygame.image.load("D:/coding/games/pang/ball4.png")]
ball_speed_y = [-18, -15, -12, -9]
balls = []
balls.append({
    "pos_x" : 263,
    "pos_y" : 50,
    "img_idx" : 0,
    "to_x" : 3,
    "to_y" : -6,
    "init_spd_y" : ball_speed_y[0]
})

# 사라질 무기, 공 정보 저장 변수
weapon_to_remove = -1
ball_to_remove = -1

# Font 정의
game_font = pygame.font.Font(None, 40)
total_time = 100
start_ticks = pygame.time.get_ticks() # 시작 시간 정의

# 게임 종료 메시지
# Time Over(실패: 시간 초과)
# Mission Complete(성공)
# Game Over(실패: 캐릭터 공에 맞음)
game_result = "Game Over!"


# 이벤트 루프
running = True
while running:
    dt = clock.tick(30)
    
    # print('fps : ' + str(clock.get_fps()))
    
    # 키보드 이벤트 처리
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            running = False
            
        if event.type == pygame.KEYDOWN:
            if event.key == pygame.K_LEFT:
                character_to_x -= character_speed
            elif event.key == pygame.K_RIGHT:
                character_to_x += character_speed
            elif event.key == pygame.K_SPACE:
                weapon_x_pos = character_x_pos + (character_width / 2) - (weapon_width / 2)
                weapon_y_pos = character_y_pos
                weapons.append([weapon_x_pos, weapon_y_pos])
    
        if event.type == pygame.KEYUP:
            if event.key == pygame.K_LEFT or event.key == pygame.K_RIGHT:
                character_to_x = 0
                
    # 캐릭터 위치 처리
    character_x_pos += character_to_x
    
    # 캐릭터 경계선 처리
    if character_x_pos < 0:
        character_x_pos = 0
    elif character_x_pos > screen_width - character_width:
        character_x_pos = screen_width - character_width
    
    # 무기 위치
    weapons = [ [w[0], w[1] - weapon_speed] for w in weapons]
    weapons = [ [w[0], w[1]] for w in weapons if w[1] > 0 ]
    
    # 공 위치
    for ball_idx, ball_val in enumerate(balls):
        ball_pos_x = ball_val["pos_x"]
        ball_pos_y = ball_val["pos_y"]
        ball_img_idx = ball_val["img_idx"]
    
        ball_size = ball_images[ball_img_idx].get_rect().size
        ball_width = ball_size[0]
        ball_height = ball_size[1]
        
        # 공 벽 경계
        if ball_pos_x < 0 or ball_pos_x > screen_width - ball_width:
            ball_val["to_x"] = ball_val["to_x"] * -1
        # 공 바닥 튀김
        if ball_pos_y >= screen_height - stage_height - ball_height:
            ball_val["to_y"] = ball_val["init_spd_y"]
        else:
            ball_val["to_y"] += 0.5
    
        ball_val["pos_x"] += ball_val["to_x"]
        ball_val["pos_y"] += ball_val["to_y"]
    
    
    # 4. 충돌 처리
    # 캐릭터 rect 정보 업데이트
    character_rect = character.get_rect()
    character_rect.left = character_x_pos
    character_rect.top = character_y_pos
    
    for ball_idx, ball_val in enumerate(balls):
        ball_pos_x = ball_val["pos_x"]
        ball_pos_y = ball_val["pos_y"]
        ball_img_idx = ball_val["img_idx"]

        # 공 rect 정보 업데이트
        ball_rect = ball_images[ball_img_idx].get_rect()
        ball_rect.left = ball_pos_x
        ball_rect.top = ball_pos_y
        
        # 공과 캐릭터 충돌 체크
        if character_rect.colliderect(ball_rect):
            running = False
            break
        
        # 공과 무기들 충돌 처리
        for weapon_idx, weapon_val in enumerate(weapons):
            weapon_pos_x = weapon_val[0]
            weapon_pos_y = weapon_val[1]
            
            # 무기 rect 정보 업데이트
            weapon_rect = weapon.get_rect()
            weapon_rect.left = weapon_pos_x
            weapon_rect.top = weapon_pos_y
    
            # 충돌 체크
            if weapon_rect.colliderect(ball_rect):
                weapon_to_remove = weapon_idx # 해당 무기 없애기 위한 값 설정
                ball_to_remove = ball_idx # 해당 공 없애기 위한 값 설정
                
                # 가장 작은 크기의 공이 아니라면 다음 단계의 공으로 나눠주기
                if ball_img_idx < 3:
                    # 현재 공 크기 정보를 가지고 옴
                    ball_width = ball_rect.size[0]
                    ball_height = ball_rect.size[1]
                    
                    # 나눠진 공 정보
                    small_ball_rect = ball_images[ball_img_idx + 1].get_rect()
                    small_ball_width = small_ball_rect.size[0]
                    small_ball_height = small_ball_rect.size[1]
                    
                    # 왼쪽으로 튕겨나가는 작은 공
                    balls.append({
                        "pos_x" : ball_pos_x + (ball_width / 2) - (small_ball_width / 2),
                        "pos_y" : ball_pos_y + (ball_height / 2) - (small_ball_height / 2),
                        "img_idx" : ball_img_idx + 1,
                        "to_x" : -3,
                        "to_y" : -6,
                        "init_spd_y" : ball_speed_y[ball_img_idx + 1]
                    })
                    
                    # 오른쪽으로 튕겨나가는 작은 공
                    balls.append({
                        "pos_x" : ball_pos_x + (ball_width / 2) - (small_ball_width / 2),
                        "pos_y" : ball_pos_y + (ball_height / 2) - (small_ball_height / 2),
                        "img_idx" : ball_img_idx + 1,
                        "to_x" : +3,
                        "to_y" : -6,
                        "init_spd_y" : ball_speed_y[ball_img_idx + 1]
                    })
                break
        else: # 계속 게임을 진행
            continue # 안쪽 for 문 조건이 맞지 않으면 continue. 바깥 for 문 계속 수행
        break # 안쪽 for 문에서 break를 만나면 여기로 진입 가능. 2중 for 문을 한번에 탈출
                
    # 충돌된 공 or 무기 없애기
    if ball_to_remove > -1:
        del balls[ball_to_remove]
        ball_to_remove = -1
        
    if weapon_to_remove > -1:
        del weapons[weapon_to_remove]
        weapon_to_remove = -1
    
    # 모든 공을 없앤 경우 게임 종료 (성공)
    if len(balls) == 0:
        game_result = "Mission Complete"
        running = False
    
    # 5. 화면에 그리기
    screen.blit(background, (0, 0)) # 배경
    
    for weapon_x_pos, weapon_y_pos in weapons: # 무기
        screen.blit(weapon, (weapon_x_pos, weapon_y_pos))
        
    for idx, val in enumerate(balls):
        ball_pos_x = val["pos_x"]
        ball_pos_y = val["pos_y"]
        ball_img_idx = val["img_idx"]
        screen.blit(ball_images[ball_img_idx], (ball_pos_x, ball_pos_y))
        
    
    screen.blit(stage, (0, screen_height - stage_height)) # 스테이지
    screen.blit(character1, (character_x_pos - 10, character_y_pos - 12)) # 캐릭터
    
    
    # 경과 시간 계산
    elapsed_time = (pygame.time.get_ticks() - start_ticks) / 1000
    timer = game_font.render("Time : {}".format(int(total_time - elapsed_time)), True, (255, 0, 100))
    screen.blit(timer, (10,10))

    # 시간 초과했다면..
    if total_time - elapsed_time <= 0:
        game_result = "Time Over"
        running = False
        break

    pygame.display.update()


# 게임 오버 메시지
msg = game_font.render(game_result, True, (255, 100, 10))
msg_rect = msg.get_rect(center = (int(screen_width / 2), int(screen_height / 2)))
screen.blit(msg, msg_rect)
pygame.display.update()


# 잠시 대기
pygame.time.delay(2000)


