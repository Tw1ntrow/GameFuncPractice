using System;

public interface IVolumeAdjustable
{
    public Action<float> OnBgmVolumeChanged { get; set; }
    public Action<float> OnSeVolumeChanged { get; set; }
}
