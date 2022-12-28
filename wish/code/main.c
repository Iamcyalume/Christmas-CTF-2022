#include <allegro.h>
#include <stdio.h>
#include <string.h>

#ifdef _WIN32
#include <synchapi.h>
#include <conio.h>
#endif

int rol(int x, int n) {
    int shift = x << n;
    shift &= 255;
    int src = x >> 8 - n;
    return shift | src;
}

int ror(int x, int n) {
    int shift = x >> n;
    int src = x << (8 - n);
    src &= 255;
    return shift | src;
}

int main(void)
{
    printf("St.Claus: I wonder what you want for Christmas.\n");
    printf("St.Claus: Can you tell me?\n\n");
    
    char input[256] = { 0, };
    signed int comp[46] = { 73, -64, 119, 97, 110, 116, 224, 964, 1423, 1280, 286, 2624, 272, 150, 1399, 111, 114, 32, 67, 104, -51, 105, 115, 116, 109, 97, -21, -173, 112, 114, 101, 115, -123, -78, 116, 44, 32, 83, 4428, 3557, 419, 4023, 1021, 7503, 9635, 927 };
    int i = 0;

    printf("You     : ");
    scanf("%[^\n]s", input);

    if (strlen(input) != 46) {
        printf("St.Claus: Hmm...\n");
        printf("St.Claus: I guess you don't deserve the present.\n");
        exit(-1);
    }
    
    for (i = 0; i < 46; i++) {
        if ((input[i] ^ i * ror(input[i], 46 - i) - rol(i, i * 5)) != comp[i]) {
            printf("St.Claus: Hmm...\n");
            printf("St.Claus: I guess you don't deserve the present.\n");
            exit(-1);
        }
    }

    if (allegro_init() != 0)
        return 1;

    BITMAP* screen2;
    \
    install_keyboard();

#ifdef _WIN32
    if (set_gfx_mode(GFX_AUTODETECT_WINDOWED, 320, 200, 0, 0) != 0) {
#else
    if (set_gfx_mode(GFX_AUTODETECT, 320, 200, 0, 0) != 0) {
#endif
        if (set_gfx_mode(GFX_SAFE, 320, 200, 0, 0) != 0) {
            set_gfx_mode(GFX_TEXT, 0, 0, 0, 0);
            return 1;
        }
    }

    screen2 = create_bitmap(screen->w, screen->h);
    if (!screen2) {
        exit(1);
    }

    set_palette(desktop_palette);

    clear_to_color(screen, makecol(0, 0, 0));
    
    printf("St.Claus: Alright... I'll give you something more useful than that!\n");
    printf("St.Claus: Wait a sec...\n");

    while (!key[KEY_ESC]) {
        char flag[46] = { 0, };
        unsigned int arr[] = { 26, 73, -3, 11, 9, 5, 83, 36, 12, -39, -4, -30, 15, 64, 9, 14, -14, 37, 46, -33, 20, -33, 2, 28, -6, 45, 6, 63, -36, 30, 24, -27, 32, 1, -13, 105, 69, 38, 27, 72, 51, 28, 61, -26, 8, 124 };
        for (i = 0; i < 46; i++) {
            flag[i] = input[i]+arr[i]-i;
        }

        char f1[13] = { 0, };
        char f2[35] = { 0, };

        for (i = 0; i < 12; i++) {
            f1[i] = flag[i];
        }


        for (i = 12; i < 46; i++) {
            f2[i - 12] = flag[i];
        }

        clear_bitmap(screen2);
        textout_centre_ex(screen2, font, f1, SCREEN_W / 2, (SCREEN_H / 7) * 2, makecol(0, 0, 0), -1);
        textout_centre_ex(screen2, font, f2, SCREEN_W / 2, (SCREEN_H / 7) * 3, makecol(0, 0, 0), -1);
        
        blit(screen2, screen, 0, 0, 0, 0, screen->w, screen->h);

        rest(1);
    }

    return 0;
    }

#ifdef _WIN32
END_OF_MAIN()
#endif