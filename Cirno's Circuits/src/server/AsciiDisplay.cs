﻿using LogicAPI.Server.Components;
using LogicWorld.ClientCode;

namespace CirnosCircuits {
	public class AsciiDisplay : LogicComponent {
		public readonly int[,] asciiTable = new int[,] {
				{0b00000,0b00000,0b00000,0b00000,0b00000,0b00000,0b00000},// space
				{0b00100,0b00100,0b00100,0b00100,0b00100,0b00000,0b00100},// !
				{0b01010,0b01010,0b01010,0b00000,0b00000,0b00000,0b00000},// "
				{0b01010,0b01010,0b11111,0b01010,0b11111,0b01010,0b01010},// #
				{0b00100,0b01111,0b10100,0b01110,0b00101,0b11110,0b00100},// $
				{0b11000,0b11001,0b00010,0b00100,0b01000,0b10011,0b00011},// %
				{0b01100,0b10010,0b10100,0b01000,0b10101,0b10010,0b01101},// &
				{0b01100,0b00100,0b01000,0b00000,0b00000,0b00000,0b00000},// '
				{0b00100,0b01000,0b10000,0b10000,0b10000,0b01000,0b00100},// (
				{0b00100,0b00010,0b00001,0b00001,0b00001,0b00010,0b00100},// )
				{0b00000,0b00100,0b10101,0b01110,0b10101,0b00100,0b00000},// *
				{0b00000,0b00100,0b00100,0b11111,0b00100,0b00100,0b00000},// +
				{0b00000,0b00000,0b00000,0b00000,0b01100,0b00100,0b01000},// ,
				{0b00000,0b00000,0b00000,0b01110,0b00000,0b00000,0b00000},// -
				{0b00000,0b00000,0b00000,0b00000,0b00000,0b00110,0b00110},// .
				{0b00001,0b00010,0b00010,0b00100,0b01000,0b01000,0b10000},// /
				{0b01110,0b10001,0b10011,0b10101,0b11001,0b10001,0b01110},// 0
				{0b00100,0b01100,0b00100,0b00100,0b00100,0b00100,0b01110},// 1
				{0b01110,0b10001,0b00001,0b00010,0b00100,0b01000,0b11111},// 2
				{0b11111,0b00010,0b00100,0b00010,0b00001,0b10001,0b01110},// 3
				{0b00010,0b00110,0b01010,0b11111,0b00010,0b00010,0b00010},// 4
				{0b11111,0b10000,0b11110,0b00001,0b00001,0b10001,0b01110},// 5
				{0b00110,0b01000,0b10000,0b11110,0b10001,0b10001,0b01110},// 6
				{0b11111,0b10001,0b00001,0b00010,0b00100,0b00100,0b00100},// 7
				{0b01110,0b10001,0b10001,0b01110,0b10001,0b10001,0b01110},// 8
				{0b01110,0b10001,0b10001,0b01111,0b00001,0b00010,0b01100},// 9
				{0b00000,0b01100,0b01100,0b00000,0b01100,0b01100,0b00000},// :
				{0b00000,0b01100,0b01100,0b00000,0b01100,0b00100,0b01000},// ;
				{0b00010,0b00100,0b01000,0b10000,0b01000,0b00100,0b00010},// <
				{0b00000,0b00000,0b11111,0b00000,0b11111,0b00000,0b00000},// =
				{0b01000,0b00100,0b00010,0b00001,0b00010,0b00100,0b01000},// >
				{0b01110,0b10001,0b00001,0b00010,0b00100,0b00000,0b00100},// ?
				{0b01110,0b10001,0b00001,0b01101,0b10101,0b10101,0b01110},// @
				{0b01110,0b10001,0b10001,0b11111,0b10001,0b10001,0b10001},// A
				{0b11110,0b10001,0b10001,0b11110,0b10001,0b10001,0b11110},// B
				{0b01110,0b10001,0b10000,0b10000,0b10000,0b10001,0b01110},// C
				{0b11100,0b10010,0b10001,0b10001,0b10001,0b10010,0b11100},// D
				{0b11111,0b10000,0b10000,0b11110,0b10000,0b10000,0b11111},// E
				{0b11111,0b10000,0b10000,0b11110,0b10000,0b10000,0b10000},// F
				{0b01110,0b10001,0b10000,0b10111,0b10001,0b10001,0b01110},// G
				{0b10001,0b10001,0b10001,0b11111,0b10001,0b10001,0b10001},// H
				{0b01110,0b00100,0b00100,0b00100,0b00100,0b00100,0b01110},// I
				{0b00111,0b00001,0b00001,0b00001,0b00001,0b10001,0b01110},// J
				{0b10001,0b10010,0b10100,0b11000,0b10100,0b10010,0b10001},// K
				{0b10000,0b10000,0b10000,0b10000,0b10000,0b10000,0b11111},// L
				{0b10001,0b11011,0b10101,0b10001,0b10001,0b10001,0b10001},// M
				{0b10001,0b10001,0b11001,0b10101,0b10011,0b10001,0b10001},// N
				{0b01110,0b10001,0b10001,0b10001,0b10001,0b10001,0b01110},// O
				{0b11110,0b10001,0b10001,0b11110,0b10000,0b10000,0b10000},// P
				{0b01110,0b10001,0b10001,0b10001,0b10101,0b10010,0b01101},// Q
				{0b11110,0b10001,0b10001,0b11110,0b10100,0b10010,0b10001},// R
				{0b01110,0b10001,0b10000,0b01110,0b00001,0b10001,0b01110},// S
				{0b11111,0b00100,0b00100,0b00100,0b00100,0b00100,0b00100},// T
				{0b10001,0b10001,0b10001,0b10001,0b10001,0b10001,0b01110},// U
				{0b10001,0b10001,0b10001,0b01010,0b01010,0b00100,0b00100},// V
				{0b10001,0b10001,0b10001,0b10001,0b10101,0b11011,0b10001},// W
				{0b10001,0b10001,0b01010,0b00100,0b01010,0b10001,0b10001},// X
				{0b10001,0b10001,0b01010,0b00100,0b00100,0b00100,0b00100},// Y
				{0b11111,0b00001,0b00010,0b00100,0b01000,0b10000,0b11111},// Z
				{0b01110,0b01000,0b01000,0b01000,0b01000,0b01000,0b01110},// [
				{0b10000,0b01000,0b01000,0b00100,0b00010,0b00010,0b00001},// \
				{0b01110,0b00010,0b00010,0b00010,0b00010,0b00010,0b01110},// ]
				{0b00100,0b01010,0b10001,0b00000,0b00000,0b00000,0b00000},// ^
				{0b00000,0b00000,0b00000,0b00000,0b00000,0b00000,0b11111},// _
				{0b01000,0b00100,0b00010,0b00000,0b00000,0b00000,0b00000},// `
				{0b00000,0b00000,0b01110,0b00001,0b01111,0b10001,0b01111},// a
				{0b10000,0b10000,0b10110,0b11001,0b10001,0b10001,0b11110},// b
				{0b00000,0b00000,0b01110,0b10000,0b10000,0b10001,0b01110},// c
				{0b00001,0b00001,0b00001,0b01101,0b10011,0b10001,0b01111},// d
				{0b00000,0b00000,0b01110,0b10001,0b11111,0b10000,0b01110},// e
				{0b00110,0b01000,0b01000,0b11100,0b01000,0b01000,0b01000},// f
				{0b00000,0b01111,0b10001,0b10001,0b01111,0b00001,0b01110},// g
				{0b10000,0b10000,0b10110,0b11001,0b10001,0b10001,0b10001},// h
				{0b00100,0b00000,0b01100,0b00100,0b00100,0b00100,0b01110},// i
				{0b00010,0b00000,0b00110,0b00010,0b00010,0b10010,0b01100},// j
				{0b10000,0b10000,0b10010,0b10100,0b11000,0b10100,0b10010},// k
				{0b01100,0b00100,0b00100,0b00100,0b00100,0b00100,0b01110},// l
				{0b00000,0b00000,0b11010,0b10101,0b10101,0b10001,0b10001},// m
				{0b00000,0b00000,0b10110,0b11001,0b10001,0b10001,0b10001},// n
				{0b00000,0b00000,0b01110,0b10001,0b10001,0b10001,0b01110},// o
				{0b00000,0b00000,0b11110,0b10001,0b11110,0b10000,0b10000},// p
				{0b00000,0b01101,0b10011,0b01111,0b00001,0b00001,0b00001},// q
				{0b00000,0b00000,0b10110,0b11001,0b10000,0b10000,0b10000},// r
				{0b00000,0b00000,0b01110,0b10000,0b01110,0b00001,0b11110},// s
				{0b01000,0b01000,0b11100,0b01000,0b01000,0b01001,0b00110},// t
				{0b00000,0b00000,0b00000,0b10001,0b10001,0b10011,0b01101},// u
				{0b00000,0b00000,0b10001,0b10001,0b10001,0b01010,0b00100},// v
				{0b00000,0b00000,0b10001,0b10101,0b10101,0b10101,0b01010},// w
				{0b00000,0b00000,0b10001,0b01010,0b00100,0b01010,0b10001},// x
				{0b00000,0b00000,0b10001,0b10001,0b01111,0b00001,0b01110},// y
				{0b00000,0b00000,0b11111,0b00010,0b00100,0b01000,0b11111},// z
				{0b00010,0b00100,0b00100,0b01000,0b00100,0b00100,0b00010},// {
				{0b00100,0b00100,0b00100,0b00100,0b00100,0b00100,0b00100},// |
				{0b01000,0b00100,0b00100,0b00010,0b00100,0b00100,0b01000},// }
				{0b00000,0b00000,0b00000,0b01101,0b10010,0b00000,0b00000},// ~
				{0b11111,0b11111,0b11111,0b11111,0b11111,0b11111,0b11111} // Full White
			};

		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			var address = utils.InputValue<int>(0, 7) - 32;
			var row = utils.InputValue<int>(7, 10) - 1;

			if (address < 0 || row < 0) {
				utils.ClearOutputs();
				return;
			}

			bool[] outer = IntToBoolArray(asciiTable[address, row]);


			for (int i = 0; i < Outputs.Count; i++) {
				Outputs[i].On = outer[i];
			}
		}

		private static bool[] IntToBoolArray(int num) {
			bool[] result = new bool[5];

			for (int i = 0; i < 5; i++) {
				result[i] = (num & (1 << i)) == 1;
			}

			return result;
		}
	}
}