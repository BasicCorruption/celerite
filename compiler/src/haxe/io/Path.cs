// Generated by Haxe 4.1.5

#pragma warning disable 109, 114, 219, 429, 168, 162
namespace haxe.io {
	public class Path : global::haxe.lang.HxObject {
		
		public Path(global::haxe.lang.EmptyObject empty) {
		}
		
		
		public Path(string path) {
			global::haxe.io.Path.__hx_ctor_haxe_io_Path(this, path);
		}
		
		
		protected static void __hx_ctor_haxe_io_Path(global::haxe.io.Path __hx_this, string path) {
			unchecked {
				switch (path) {
					case ".":
					case "..":
					{
						__hx_this.dir = path;
						__hx_this.file = "";
						return;
					}
					
					
				}
				
				int c1 = global::haxe.lang.StringExt.lastIndexOf(path, "/", default(global::haxe.lang.Null<int>));
				int c2 = global::haxe.lang.StringExt.lastIndexOf(path, "\\", default(global::haxe.lang.Null<int>));
				if (( c1 < c2 )) {
					__hx_this.dir = global::haxe.lang.StringExt.substr(path, 0, new global::haxe.lang.Null<int>(c2, true));
					path = global::haxe.lang.StringExt.substr(path, ( c2 + 1 ), default(global::haxe.lang.Null<int>));
					__hx_this.backslash = true;
				}
				else if (( c2 < c1 )) {
					__hx_this.dir = global::haxe.lang.StringExt.substr(path, 0, new global::haxe.lang.Null<int>(c1, true));
					path = global::haxe.lang.StringExt.substr(path, ( c1 + 1 ), default(global::haxe.lang.Null<int>));
				}
				else {
					__hx_this.dir = null;
				}
				
				int cp = global::haxe.lang.StringExt.lastIndexOf(path, ".", default(global::haxe.lang.Null<int>));
				if (( cp != -1 )) {
					__hx_this.ext = global::haxe.lang.StringExt.substr(path, ( cp + 1 ), default(global::haxe.lang.Null<int>));
					__hx_this.file = global::haxe.lang.StringExt.substr(path, 0, new global::haxe.lang.Null<int>(cp, true));
				}
				else {
					__hx_this.ext = null;
					__hx_this.file = path;
				}
				
			}
		}
		
		
		public static string withoutDirectory(string path) {
			global::haxe.io.Path s = new global::haxe.io.Path(((string) (path) ));
			s.dir = null;
			return s.toString();
		}
		
		
		public static string directory(string path) {
			global::haxe.io.Path s = new global::haxe.io.Path(((string) (path) ));
			if (( s.dir == null )) {
				return "";
			}
			
			return s.dir;
		}
		
		
		public static string @join(global::Array<string> paths) {
			unchecked {
				global::Array<string> ret = new global::Array<string>(new string[]{});
				{
					int _g = 0;
					int _g1 = paths.length;
					while (( _g < _g1 )) {
						int i = _g++;
						string elt = ((string) (paths.__a[i]) );
						if (( ( elt != null ) && ( elt != "" ) )) {
							ret.push(elt);
						}
						
					}
					
				}
				
				global::Array<string> paths1 = ret;
				if (( paths1.length == 0 )) {
					return "";
				}
				
				string path = paths1[0];
				{
					int _g2 = 1;
					int _g3 = paths1.length;
					while (( _g2 < _g3 )) {
						int i1 = _g2++;
						path = global::haxe.io.Path.addTrailingSlash(path);
						path = global::haxe.lang.Runtime.concat(path, paths1[i1]);
					}
					
				}
				
				return global::haxe.io.Path.normalize(path);
			}
		}
		
		
		public static string normalize(string path) {
			unchecked {
				string slash = "/";
				path = global::haxe.lang.StringExt.split(path, "\\").@join(slash);
				if (( path == slash )) {
					return slash;
				}
				
				global::Array<string> target = new global::Array<string>(new string[]{});
				{
					int _g = 0;
					global::Array<string> _g1 = global::haxe.lang.StringExt.split(path, slash);
					while (( _g < _g1.length )) {
						string token = _g1[_g];
						 ++ _g;
						if (( ( ( token == ".." ) && ( target.length > 0 ) ) && ( target[( target.length - 1 )] != ".." ) )) {
							string __temp_expr1 = global::haxe.lang.Runtime.toString((target.pop()).toDynamic());
						}
						else if (( token == "" )) {
							if (( ( target.length > 0 ) || global::haxe.lang.Runtime.eq((global::haxe.lang.StringExt.charCodeAt(path, 0)).toDynamic(), 47) )) {
								target.push(token);
							}
							
						}
						else if (( token != "." )) {
							target.push(token);
						}
						
					}
					
				}
				
				string tmp = target.@join(slash);
				global::StringBuf acc = new global::StringBuf();
				bool colon = false;
				bool slashes = false;
				{
					int _g2_offset = 0;
					string _g2_s = tmp;
					while (( _g2_offset < _g2_s.Length )) {
						string s = _g2_s;
						int index = _g2_offset++;
						int c = ( (( ((uint) (index) ) < s.Length )) ? (((int) (s[index]) )) : (-1) );
						if (( ( c >= 55296 ) && ( c <= 56319 ) )) {
							int index1 = ( index + 1 );
							c = ( ( ( c - 55232 ) << 10 ) | ( (( (( ((uint) (index1) ) < s.Length )) ? (((int) (s[index1]) )) : (-1) )) & 1023 ) );
						}
						
						int c1 = c;
						if (( c1 >= 65536 )) {
							 ++ _g2_offset;
						}
						
						int c2 = c1;
						switch (c2) {
							case 47:
							{
								if ( ! (colon) ) {
									slashes = true;
								}
								else {
									int i = c2;
									{
										colon = false;
										if (slashes) {
											acc.b.Append(((string) ("/") ));
											slashes = false;
										}
										
										acc.addChar(i);
									}
									
								}
								
								break;
							}
							
							
							case 58:
							{
								acc.b.Append(((string) (":") ));
								colon = true;
								break;
							}
							
							
							default:
							{
								int i1 = c2;
								{
									colon = false;
									if (slashes) {
										acc.b.Append(((string) ("/") ));
										slashes = false;
									}
									
									acc.addChar(i1);
								}
								
								break;
							}
							
						}
						
					}
					
				}
				
				return acc.b.ToString();
			}
		}
		
		
		public static string addTrailingSlash(string path) {
			unchecked {
				if (( path.Length == 0 )) {
					return "/";
				}
				
				int c1 = global::haxe.lang.StringExt.lastIndexOf(path, "/", default(global::haxe.lang.Null<int>));
				int c2 = global::haxe.lang.StringExt.lastIndexOf(path, "\\", default(global::haxe.lang.Null<int>));
				if (( c1 < c2 )) {
					if (( c2 != ( path.Length - 1 ) )) {
						return global::haxe.lang.Runtime.concat(path, "\\");
					}
					else {
						return path;
					}
					
				}
				else if (( c1 != ( path.Length - 1 ) )) {
					return global::haxe.lang.Runtime.concat(path, "/");
				}
				else {
					return path;
				}
				
			}
		}
		
		
		public string dir;
		
		public string file;
		
		public string ext;
		
		public bool backslash;
		
		public virtual string toString() {
			return global::haxe.lang.Runtime.concat(global::haxe.lang.Runtime.concat((( (( this.dir == null )) ? ("") : (global::haxe.lang.Runtime.concat(this.dir, (( (this.backslash) ? ("\\") : ("/") )))) )), this.file), (( (( this.ext == null )) ? ("") : (global::haxe.lang.Runtime.concat(".", this.ext)) )));
		}
		
		
		public override object __hx_setField(string field, int hash, object @value, bool handleProperties) {
			unchecked {
				switch (hash) {
					case 1212528822:
					{
						this.backslash = global::haxe.lang.Runtime.toBool(@value);
						return @value;
					}
					
					
					case 5049505:
					{
						this.ext = global::haxe.lang.Runtime.toString(@value);
						return @value;
					}
					
					
					case 1136381564:
					{
						this.file = global::haxe.lang.Runtime.toString(@value);
						return @value;
					}
					
					
					case 4996429:
					{
						this.dir = global::haxe.lang.Runtime.toString(@value);
						return @value;
					}
					
					
					default:
					{
						return base.__hx_setField(field, hash, @value, handleProperties);
					}
					
				}
				
			}
		}
		
		
		public override object __hx_getField(string field, int hash, bool throwErrors, bool isCheck, bool handleProperties) {
			unchecked {
				switch (hash) {
					case 946786476:
					{
						return ((global::haxe.lang.Function) (new global::haxe.lang.Closure(this, "toString", 946786476)) );
					}
					
					
					case 1212528822:
					{
						return this.backslash;
					}
					
					
					case 5049505:
					{
						return this.ext;
					}
					
					
					case 1136381564:
					{
						return this.file;
					}
					
					
					case 4996429:
					{
						return this.dir;
					}
					
					
					default:
					{
						return base.__hx_getField(field, hash, throwErrors, isCheck, handleProperties);
					}
					
				}
				
			}
		}
		
		
		public override object __hx_invokeField(string field, int hash, object[] dynargs) {
			unchecked {
				switch (hash) {
					case 946786476:
					{
						return this.toString();
					}
					
					
					default:
					{
						return base.__hx_invokeField(field, hash, dynargs);
					}
					
				}
				
			}
		}
		
		
		public override void __hx_getFields(global::Array<string> baseArr) {
			baseArr.push("backslash");
			baseArr.push("ext");
			baseArr.push("file");
			baseArr.push("dir");
			base.__hx_getFields(baseArr);
		}
		
		
		public override string ToString(){
			return this.toString();
		}
		
		
	}
}


