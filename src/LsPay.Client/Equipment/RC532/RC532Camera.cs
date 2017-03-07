using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LsPay.Client.Equipment.RC532
{
    public class RC532Camera
    {
        //        参数：HWND hwnd; 接收解码信息 的窗口句柄。
        //函数功能：将接收解码信息 的窗口句柄传给dll

        [DllImport("dll_camera.dll", EntryPoint = "GetAppHandle")]
        public static extern void GetAppHandle(IntPtr hWnd);
        //函数功能：启动设备。
        //返回值：true 设备启动成功
        //false设备启动失败

        [DllImport("dll_camera.dll", EntryPoint = "StartDevice")]
        public static extern Boolean StartDevice();
        //释放摄像头。
        [DllImport("dll_camera.dll", EntryPoint = "ReleaseDevice")]
        public static extern void ReleaseDevice();
        //参数：bqr， true时，qr引擎开启；false时qr引擎关闭
        //函数功能：设置QR引擎状态
        [DllImport("dll_camera.dll", EntryPoint = "setQRable")]
        public static extern void setQRable(bool bqr);
        //参数：bdm ，true时，dm引擎开启；false时dm引擎关闭
        //函数功能：设置DM引擎状态
        [DllImport("dll_camera.dll", EntryPoint = "setDMable")]
        public static extern void setDMable(bool bdm);
        //        参数：bbarcode， true时，一维引擎开启；false时一维引擎关闭
        //函数功能：设置一维引擎状态
        [DllImport("dll_camera.dll", EntryPoint = "setBarcode")]
        public static extern void setBarcode(bool bbarcode);

        //参数：int BeepTime  蜂鸣时间 单位：ms
        //函数功能：设定解码成功后的蜂鸣时间
        [DllImport("dll_camera.dll", EntryPoint = "SetBeepTime")]
        public static extern void SetBeepTime(int BeepTime);

        //参数：char *aa，获取解码信息的指针
        //函数功能：获取解码信息
        [DllImport("dll_camera.dll", EntryPoint = "GetDecodeString")]
        public static extern void GetDecodeString(StringBuilder sb);

        //函数功能：查找设备
        //返回值：1，表示查找设备成功
        //        -1，表示查找设备失败。
        [DllImport("dll_camera.dll", EntryPoint = "GetDevice")]
        public static extern int GetDevice();

        //函数功能：人为拔出设备时，需要释放的摄像头资源。
        //返回值：无
        [DllImport("dll_camera.dll", EntryPoint = "ReleaseLostDevice")]
        public static extern void ReleaseLostDevice();
    }
}
