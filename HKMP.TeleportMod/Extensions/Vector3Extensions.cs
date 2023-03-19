using System;
using System.Globalization;
using HKMP_Teleport;
using UnityEngine;

namespace HKMP.TeleportMod;

public static class Vector3Extensions
{
    public static string Vector3ToString(this Vector3 vector3)
    {
        return $"({vector3.x.ToString(CultureInfo.InvariantCulture)}, {vector3.x.ToString(CultureInfo.InvariantCulture)}, {vector3.x.ToString(CultureInfo.InvariantCulture)})";
    }
    public static Vector3 StringToVector3(this string sVector)
    {
        if (string.IsNullOrEmpty(sVector)) return Vector3.zero;
        try
        {
            // Remove the parentheses
            if (sVector.StartsWith("(") && sVector.EndsWith(")"))
            {
                sVector = sVector.Substring(1, sVector.Length - 2);
            }
                
            // split the items
            string[] sArray = sVector.Split(',');

            // store as a Vector3
            Vector3 result = new Vector3(
                float.Parse(sArray[0], CultureInfo.InvariantCulture),
                float.Parse(sArray[1], CultureInfo.InvariantCulture),
                float.Parse(sArray[2], CultureInfo.InvariantCulture));

            return result;
        }
        catch (Exception e)
        {
            HKMP_TeleportMod.Instance.Log(e);
            return Vector3.zero;
        }
    }
}