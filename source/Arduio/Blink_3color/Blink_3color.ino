
int led_3 = 3;
int led_7 = 7;
int led_11 = 11;
// int DELAY_TIME = 200;
#define DELAY_TIME 300


// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  //pinMode(LED_BUILTIN, OUTPUT);
  pinMode(led_3, OUTPUT);
  pinMode(led_7, OUTPUT);
  pinMode(led_11, OUTPUT);
}

// the loop function runs over and over again forever
void loop() {
  digitalWrite(led_3, HIGH);
  delay(DELAY_TIME);
  digitalWrite(led_3, LOW);

  digitalWrite(led_7, HIGH);
  delay(DELAY_TIME);
  digitalWrite(led_7, LOW);

  digitalWrite(led_11, HIGH);
  delay(DELAY_TIME);
  digitalWrite(led_11, LOW);
}
