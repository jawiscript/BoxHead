﻿using System;
using System.Collections.Generic;
using Tao.Sdl;

class MapCreatorScreen : Screen
{
    private Image background;
    private Image wall;
    private Image spawn;
    private Image barrel;

    private IntPtr helpText;
    private IntPtr finishText;

    private IntPtr[] nums;
    private bool displayHelp;
    private int mouseClickX, mouseClickY;
    private int mouseX, mouseY;
    int selectedImage;

    private List<Obstacle> newObstacles;

    public MapCreatorScreen(Hardware hardware) : base(hardware)
    {
        background = new Image("imgs/others/floor.png", 1280, 720);
        wall = new Image("imgs/obstacles/wall.png", 40, 40);
        spawn = new Image("imgs/obstacles/spawnpoint.png", 50, 50);
        barrel = new Image("imgs/obstacles/barrel.png", 36, 36);
        helpText = new IntPtr();
        nums = new IntPtr[3];
        initialiceTexts();
        displayHelp = false;
        selectedImage = 1;
        newObstacles = new List<Obstacle>();
    }

    public override void Show()
    {
        background.MoveTo(0, 0);
        wall.MoveTo(
            (GameController.SCREEN_WIDTH / 2) - 60, 
            GameController.SCREEN_HEIGHT - 90);
        spawn.MoveTo(
            (GameController.SCREEN_WIDTH / 2 + 30),
            GameController.SCREEN_HEIGHT - 90);
        barrel.MoveTo(
            (GameController.SCREEN_WIDTH / 2) + 140, 
            GameController.SCREEN_HEIGHT - 90);

        do
        {
            // Draw everything.
            hardware.ClearScreen();
            hardware.DrawImage(background);

            foreach (Obstacle o in newObstacles)
            {
                o.Image.MoveTo(o.X, o.Y);
                hardware.DrawImage(o.Image);
            }

            if (!displayHelp)
            {
                hardware.WriteText(
                    helpText, (GameController.SCREEN_WIDTH / 2) - 50,
                    GameController.SCREEN_HEIGHT - 50);
            }
            else
            {
                hardware.WriteText(
                    finishText, GameController.SCREEN_WIDTH - 150, 10);

                short xPos = (GameController.SCREEN_WIDTH / 2) - 50;
                for (int i = 0; i < nums.Length; i++)
                {
                    hardware.WriteText(
                        nums[i], xPos, GameController.SCREEN_HEIGHT - 150);
                    xPos += 100;
                    hardware.DrawImage(wall);
                    hardware.DrawImage(spawn);
                    hardware.DrawImage(barrel);
                }
            }
            
            hardware.UpdateScreen();

            // Check user input.

            hardware.GetEvents(
                out mouseX, out mouseY, out mouseClickX, out mouseClickY);

            if (mouseClickX != -1 && mouseClickY != -1)
                addObstacle();

            if (hardware.IsKeyPressed(Hardware.KEY_TAB))
                displayHelp = !displayHelp;
            else if (hardware.IsKeyPressed(Hardware.KEY_1))
                selectedImage = 1;
            else if (hardware.IsKeyPressed(Hardware.KEY_2))
                selectedImage = 2;
            else if (hardware.IsKeyPressed(Hardware.KEY_3))
                selectedImage = 3;
        }
        while (!isFinished());
    }

    private void addObstacle()
    {
        switch (selectedImage)
        {
            case 1:
                newObstacles.Add(
                    new Wall((short)(mouseClickX), (short)(mouseClickY)));
                break;
            case 2:
                newObstacles.Add(
                    new SpawnPoint((short)(mouseClickX), (short)(mouseClickY)));
                break;
            case 3:
                newObstacles.Add(
                    new Barrel((short)(mouseClickX), (short)(mouseClickY)));
                break;
            default:
                break;
        }
    }

    private bool isFinished()
    {
        return hardware.IsKeyPressed(Hardware.KEY_ESC);
    }

    private void initialiceTexts()
    {
        helpText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/Creepster-Regular.ttf", 20).GetFontType(),
                "Press TAB to show help", hardware.Red);

        finishText = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/Creepster-Regular.ttf", 18).GetFontType(),
                "Press ESC to finish", hardware.Red);

        for (int i = 1; i <= nums.Length; i++)
            nums[i-1] = SdlTtf.TTF_RenderText_Solid(
                new Font("fonts/Creepster-Regular.ttf", 40).GetFontType(),
                i.ToString(), hardware.Red);
    }
}
