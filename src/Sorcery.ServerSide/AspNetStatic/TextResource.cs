// Licensed under MIT, copyright Mateusz Gienieczko, all rights reserved.

using AspNetStatic;

namespace Sorcery.ServerSide.AspNetStatic;

[Serializable]
internal class TextResource(string route) : BinResource(route)
{
}
