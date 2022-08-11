# Gamba

This is a program to simulate betting on the Roulette from the video game [Rust](https://rust.facepunch.com/).

It was created as a way to quickly simulate gambling strategies to see the outcomes.

Wheel:
- 25 spots
- Numbers
  - 12  #1s
  -  6  #3s
  -  4  #5s
  -  2 #10s
  -  1 #20
-  Payouts
    -  #1  ->  2x
    -  #3  ->  4x
    -  #5  ->  6x
    -  #10 -> 11x
    -  #20 -> 21x

Takeaway: House always wins, don't gamble.

Shortfalls: 

1. The numbers are generated using the Random class, and may not accurately mirror the implementation done by the game developers.
2. Does not support additional funds, just a starting amount.
3. Only allows for one bet per spin.
