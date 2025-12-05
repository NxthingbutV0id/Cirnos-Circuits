using System;
using LogicAPI.Server.Components;

namespace CirnosCircuits;

public abstract class DisplayUnit : LogicComponent {
    private IOHandler ioHandler;
    private bool[,] displayData, displayBuffer;
    private bool prevClk;
    private bool clk;
    protected abstract int width { get; }
    protected abstract int height { get; }
    private int pixelOutIndex;
    private byte x, y;
    private bool pixelValue, clearScreen, clearBuffer, pushBuffer, writePixel;
    private readonly int[] indexes = new int[7];
    private int bitMask;

    protected override void Initialize() {
        ioHandler = new IOHandler(Inputs, Outputs);
        displayData = new bool[width, height];
        displayBuffer = new bool[width, height]; // TODO: Might be unnecessary, but it makes the logic easier
        prevClk = false;
        pixelOutIndex = Outputs.Count - 1;
        SetIndexes();
        bitMask = (1 << indexes[0]) - 1; // Assuming the number of bits for x is the same as for y
    }

    protected override void DoLogicUpdate() {
        x = (byte)(ioHandler.GetInputAs<byte>() & bitMask);
        y = (byte)(ioHandler.GetInputAs<byte>(indexes[0]) & bitMask);
        pixelValue = Inputs[indexes[1]].On;
        clearScreen = Inputs[indexes[2]].On;
        clearBuffer = Inputs[indexes[3]].On;
        pushBuffer = Inputs[indexes[4]].On;
        writePixel = Inputs[indexes[5]].On;
        clk = Inputs[indexes[6]].On;
        if (!prevClk && clk) {
            if (writePixel) WritePixelToBuffer();
            if (clearScreen) ClearScreen();
            if (clearBuffer) ClearBuffer();
            if (pushBuffer) PushBufferToDisplay();
        }
        prevClk = clk;

        Outputs[pixelOutIndex].On = displayBuffer[x, y];
    }

    // These values are constant, so we can calculate them once at initialization
    private void SetIndexes() {
        var xSize = (int) Math.Ceiling(Math.Log2(width));
        var ySize = (int) Math.Ceiling(Math.Log2(height));
        indexes[0] = xSize;
        indexes[1] = xSize + ySize;
        indexes[2] = xSize + ySize + 1; // Clear Screen
        indexes[3] = xSize + ySize + 2; // Clear Buffer
        indexes[4] = xSize + ySize + 3; // Push Buffer
        indexes[5] = xSize + ySize + 4; // Write Pixel
        indexes[6] = xSize + ySize + 5; // Clock
    }

    // If the Push Buffer pin is high, The buffer will be pushed to the display.
    // The buffer will not be cleared
    private void PushBufferToDisplay() {
        for (var i = 0; i < width; i++) {
            for (var j = 0; j < height; j++) { displayData[i, j] = displayBuffer[i, j]; }
        }
        ioHandler.OutputBoolArray(displayData); // Update outputs
    }
    
    // If the Write Pixel pin is high, The pixel will be written to the buffer.
    // Note that clearing the buffer on the same tick will override this.
    private void WritePixelToBuffer() {
        if (x < width && y < height) {
            displayBuffer[x, y] = pixelValue;
        }
    }
    
    // If the Clear Buffer pin is high, The buffer will be cleared.
    // Clear Buffer has a higher priority than Write Pixel.
    private void ClearBuffer() {
        for (var i = 0; i < width; i++) {
            for (var j = 0; j < height; j++) { displayBuffer[i, j] = false; }
        }
    }
    
    // If the Clear Screen pin is high, The display will be cleared.
    // This does not affect the buffer.
    private void ClearScreen() {
        for (var i = 0; i < width; i++) {
            for (var j = 0; j < height; j++) { displayData[i, j] = false; }
        }
        ioHandler.ClearOutputs(); // Update outputs
    }
}
// Too big, will crash saves
public class SquareDisplay32 : DisplayUnit {
    protected override int width => 32;
    protected override int height => 32;
}

// Too big, will crash saves
public class SquareDisplay64 : DisplayUnit {
    protected override int width => 64;
    protected override int height => 64;
}

// Massive... Will cause major lag
public class GameBoyDisplay : DisplayUnit {
    protected override int width => 160;
    protected override int height => 144;
    
}