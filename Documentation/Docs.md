# Cirno's Circuits Documentation (WIP)


To avoid any confusion, these are general things I try to follow in all components:

- The least significant bit is always on the right when looking from the top down.
- If there is a clock input, it is always active on the rising edge.

I am only going to include more complex components in this, as the others should be self-explanatory.
Images will be included later.

### Arithmetic Components (Multiplier, Divider)
- Upper pins are for input A
- Lower pins are for input B
- Multiplier performs A * B
- Divider performs A / B
- The extra pin is for signed and unsigned operation. If the pin is high, the operation is signed

### RAM
On the top, from the right most pin is:
- Clock
- Write
- Reset
- Enable

On the input side:
- Upper pins are the address
- Lower pins are the data

### Printers
Printers display the input value in the console on the rising edge of the clock. The input is always treated as unsigned, and the printer will display the value in decimal, hexadecimal, or floating point depending on the printer type.
