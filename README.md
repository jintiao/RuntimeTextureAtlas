# RuntimeTextureAtlas

Pack multiple textures into one rendertexture at runtime.

## How To Use
```c#
RawImage image;
Texture texture;
...
image.PackTexture(texture);
```

## Usecase

for a UI Scene which have 5 RawImage controls:
![screenshot](/Image/screenshot.png)

RTA pack all five texture into a rendertexture:
![rendertexture](/Image/rendertexture.png)

4 drawcall saved.

|                without RTA                  |                   with RTA                 |
| ------------------------------------------- | -------------------------------------------|
| ![without RTA](/Image/drawcall-without.png) | ![with RTA](/Image/drawcall-with.png)      |

