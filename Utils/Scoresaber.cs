/**
 * This file incorporates third party material from the project listed below.
 * 
 * CS_BeatSaber_Camera2
 * Repository: https://github.com/kinsi55/CS_BeatSaber_Camera2
 * 
 * MIT License

Copyright (c) 2021 Kinsi

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using HarmonyLib;
using System.Reflection;

namespace PlayFirst.Utils
{
    public static class ScoresaberUtil
    {
        static MethodBase ScoreSaber_playbackEnabled = AccessTools.Method("ScoreSaber.Core.ReplaySystem.HarmonyPatches.PatchHandleHMDUnmounted:Prefix");

        /**
         * This static method returns a boolean value whether the game current state is on ScoreSaber replay view
         * If ScoreSaber plugin does not exist, ScoreSaber_playbackEnabled is null and this static method returns false
         */
        public static bool IsInReplay()
        {
            try
            {
                return ScoreSaber_playbackEnabled != null && (bool)ScoreSaber_playbackEnabled.Invoke(null, null) == false;
            }
            catch { }
            return false;
        }
    }
}
