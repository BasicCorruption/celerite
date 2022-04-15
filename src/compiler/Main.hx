package;

import sys.io.File;
import haxe.io.Path;

using StringTools;

class Main {
	public static var args = Sys.args();

	public static function main() {
		if (args.length > 0) {
			switch (args[0]) {
				case "--help":
					Sys.println("Usage: celerite <target> <file> [options]\n");
					Sys.println("Targets:\n");
					Sys.println("--interp | Interpret the celerite file");
					Sys.println("--asm    | Compile the celerite file into an assembly file");
					Sys.println("--lua    | Compile the celerite file into a lua file");
				case "--interp":
					var code = Files.getLines(args[1]);
					for (lines in code) {
						if (lines.startsWith("out.add(")) {
							var s = lines.substring(8);
							var h = "";
							h = s.substring(0, s.length - 1);
							Sys.print(Hex.hexArray[Std.parseInt(h)]);
						}
					}
				case "--asm":
				case "--lua":
					var code = Files.getLines(args[1]);
					FileManager.setType(LUA);

					if (args[2] == null) {
						Sys.println("Please specify a directory");
						Sys.println("Usage: celerite --lua <sourcefile> <outputdirectory>");
						return;
					}

					for (lines in code) {
						if (lines.startsWith("out.add(")) {
							var s = lines.substring(8);
							var h = "";
							h = s.substring(0, s.length - 1);
							FileManager.append('print("${Hex.hexArray[Std.parseInt(h)]}")\n');
						}
					}
					FileManager.save(Path.directory(args[2]), Path.withoutDirectory(args[1]).substring(0, Path.withoutDirectory(args[1]).length - 3));
				case "--compile":
					var code = Files.getLines(args[1]);
					FileManager.setType(CELERITE);

					if (args[2] == null) {
						Sys.println("Please specify a directory");
						Sys.println("Usage: celerite --compile <sourcefile> <outputdirectory>");
						return;
					}

					for (lines in code) {
						if (lines.startsWith("out.add(")) {
							var s = lines.substring(8);
							var h = "";
							h = s.substring(0, s.length - 2);
							FileManager.append(h + " ");
						}
					}
					FileManager.save(Path.directory(args[2]), Path.withoutDirectory(args[1]).substring(0, Path.withoutDirectory(args[1]).length - 3));
				default:
					var code = Files.getLines(args[0]);
					for (lines in code) {
						if (lines.startsWith("out.add(")) {
							var s = lines.substring(8);
							var h = "";
							h = s.substring(0, s.length - 1);
							Sys.print(Hex.hexArray[Std.parseInt(h)]);
						}
					}
			}
		} else {
			Sys.println("Usage: celerite <target> <file> [options]");
			return;
		}
	}
}

class Files {
	public static function getLines(path:String) {
		var p = Path.normalize(path);
		var c = File.getContent(p);
		var l = c.split("\n");
		return l;
	}
}

class Hex {
	public static var hexArray = [
		0x0 => null, 0x1 => null, 0x2 => null, 0x3 => null, 0x4 => null, 0x5 => null, 0x6 => null, 0x7 => null, 0x8 => null, 0x9 => null, 0xA => null,
		0xB => null, 0xC => null, 0xD => "\n", 0xE => null, 0xF => null, 0x10 => null, 0x11 => null, 0x12 => null, 0x13 => null, 0x14 => null, 0x15 => null,
		0x16 => null, 0x17 => null, 0x18 => null, 0x19 => null, 0x1A => null, 0x1B => null, 0x1C => null, 0x1D => null, 0x1E => null, 0x1F => null,
		0x20 => " ", 0x21 => "!", 0x22 => "\"", 0x23 => "#", 0x24 => "$", 0x25 => "%", 0x26 => "&", 0x27 => "'", 0x28 => "(", 0x29 => ")", 0x2A => "*",
		0x2B => "+", 0x2C => ",", 0x2D => "-", 0x2E => ".", 0x2F => "/", 0x30 => "0", 0x31 => "1", 0x32 => "2", 0x33 => "3", 0x34 => "4", 0x35 => "5",
		0x36 => "6", 0x37 => "7", 0x38 => "8", 0x39 => "9", 0x3A => ":", 0x3B => ";", 0x3C => "<", 0x3D => "=", 0x3E => ">", 0x3F => "?", 0x40 => "@",
		0x41 => "A", 0x42 => "B", 0x43 => "C", 0x44 => "D", 0x45 => "E", 0x46 => "F", 0x47 => "G", 0x48 => "H", 0x49 => "I", 0x4A => "J", 0x4B => "K",
		0x4C => "L", 0x4D => "M", 0x4E => "N", 0x4F => "O", 0x50 => "P", 0x51 => "Q", 0x52 => "R", 0x53 => "S", 0x54 => "T", 0x55 => "U", 0x56 => "V",
		0x57 => "W", 0x58 => "X", 0x59 => "Y", 0x5A => "Z", 0x5B => "[", 0x5C => "\\", 0x5D => "]", 0x5E => "^", 0x5F => "_", 0x60 => "`", 0x61 => "a",
		0x62 => "b", 0x63 => "c", 0x64 => "d", 0x65 => "e", 0x66 => "f", 0x67 => "g", 0x68 => "h", 0x69 => "i", 0x6A => "j", 0x6B => "k", 0x6C => "l",
		0x6D => "m", 0x6E => "n", 0x6F => "o", 0x70 => "p", 0x71 => "q", 0x72 => "r", 0x73 => "s", 0x74 => "t", 0x75 => "u", 0x76 => "v", 0x77 => "w",
		0x78 => "x", 0x79 => "y", 0x7A => "z", 0x7B => "{", 0x7C => "|", 0x7D => "}", 0x7E => "~", 0x7F => null
	];
}
