﻿
public interface IInputHandler
{
    bool Left();
    bool Right();
    bool HoldingLeft();
    bool HoldingRight();
    bool ReleaseLeft();
    bool ReleaseRight();
    bool Action();
    bool HoldingDown();
    bool Down();
    bool ReleaseDown();
}
