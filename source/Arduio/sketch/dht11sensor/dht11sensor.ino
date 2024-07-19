/*
https://blog.naver.com/boilmint7/220928870337
https://blog.naver.com/compass1111/221230246071
https://commons.wikimedia.org/wiki/File:Sensore_DHT11_connesso_ad_Arduino.svg
*/


#include <TimeLib.h>
#include <DHT11.h>

DHT11 dht11(A0);
void setup()
{
  Serial.begin(9600);
  setTime(19,11,0,16,7,24);
  time_t t = now();
  Serial.println("==========================");
  Serial.println("아두이노를 시작합니다.");
  Serial.print("시작시간 ");
  Serial.print(year(t));
  Serial.print("-");
  Serial.print(month(t));
  Serial.print("-");
  Serial.print(day(t));
  Serial.print(" ");
  Serial.print(hour(t));
  Serial.print(":");
  Serial.print(minute(t));
  Serial.print(":");
  Serial.println(second(t));
  Serial.println("==========================");
}

void loop()
{
  float temp, humi; 
  int result = dht11.read(humi, temp);

  if (result == 0)
  {
    time_t t = now();

    Serial.print(year(t));
    Serial.print("-");
    Serial.print(month(t));
    Serial.print("-");
    Serial.print(day(t));
    Serial.print(" ");
    Serial.print(hour(t));
    Serial.print(":");
    Serial.print(minute(t));
    Serial.print(":");
    Serial.print(second(t));
    
    Serial.print(" // 온도 : "); 
    Serial.print(temp);
    Serial.print(" // 습도 : ");
    Serial.print(humi);
    Serial.println();
  }
  else
  {
    Serial.println();
    Serial.print("Error No :");
    Serial.print(result);
    Serial.println();
  }

  delay(DHT11_RETRY_DELAY);
}



