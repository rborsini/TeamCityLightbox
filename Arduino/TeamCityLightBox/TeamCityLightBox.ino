
int LED_1 = 3;
int LED_2 = 4;
int LED_3 = 5;

String lastCommand = "";


void setup() {
  
  pinMode(LED_1, OUTPUT);
  pinMode(LED_2, OUTPUT);
  pinMode(LED_3, OUTPUT);

  Serial.begin(9600);
}

void loop() {

  if(Serial.available()) {
    lastCommand = Serial.readString();
    Serial.print(lastCommand);
  }

  if(lastCommand.startsWith("slide")) {
    slide();
  } else if(lastCommand.startsWith("blink")) {
    blinkAll();
  } else {
    off();
  }

  Serial.write("loop\n");
                            
}

void slide() {

  digitalWrite(LED_1, LOW);
  digitalWrite(LED_2, LOW); 
  digitalWrite(LED_3, LOW); 
  delay(500);

  digitalWrite(LED_1, HIGH);
  delay(500);
  digitalWrite(LED_2, HIGH);
  delay(500);
  digitalWrite(LED_3, HIGH);
  delay(500);

}

void blinkAll() {

  digitalWrite(LED_1, LOW);
  digitalWrite(LED_2, LOW); 
  digitalWrite(LED_3, LOW); 
  delay(500);

  digitalWrite(LED_1, HIGH);
  digitalWrite(LED_2, HIGH);
  digitalWrite(LED_3, HIGH);
  delay(500);

}

void blink(int led) {

  digitalWrite(led, LOW);
  delay(500);

  digitalWrite(led, HIGH);
  delay(500);

}

void off() {

  digitalWrite(LED_1, LOW);
  digitalWrite(LED_2, LOW); 
  digitalWrite(LED_3, LOW); 

}
