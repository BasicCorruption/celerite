package;

import haxe.io.Path;
import sys.io.File;

class FileManager {
	public static var content:String = "";
	public static var ext:String = "";

	public static function setType(type:FileType) {
		switch (type) {
			case ASSEMBLY:
				ext = ".asm";
			case LUA:
				ext = ".lua";
			case CELERITE:
				ext = ".cec";
		}
	}

	public static function append(text:String) {
		content += text;
	}

	public static function save(path:String, name:String) {
		File.saveContent(Path.join([path, name + ext]), content);
		Sys.println("File saved to " + Path.join([path, name + ext]));
	}
}

enum FileType {
	CELERITE;
	LUA;
	ASSEMBLY;
}
