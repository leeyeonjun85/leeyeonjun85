/*
  DigitalReadSerial

  Reads a digital input on pin 2, prints the result to the Serial Monitor

  This example code is in the public domain.

  https://www.arduino.cc/en/Tutorial/BuiltInExamples/DigitalReadSerial
*/

#define LED 10
#define BUTTON 2

// the setup routine runs once when you press reset:
void setup() {
  pinMode(LED, OUTPUT);
  pinMode(BUTTON, INPUT);
  // Serial.begin(9600);
}

// the loop routine runs over and over again forever:
void loop() {
  if(digitalRead(BUTTON) == HIGH){
    digitalWrite(LED, HIGH);
    delay(1);
    digitalWrite(LED, LOW);
    //Serial.println(digitalRead(BUTTON));
  }

  // if(digitalRead(BUTTON) == LOW){
  //   digitalWrite(LED, LOW);
  //   delay(1);
  //   digitalWrite(LED, HIGH);
  // }
}
