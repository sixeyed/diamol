package iotd.services;

import java.util.ArrayList;

import iotd.models.Image;

public interface CacheService {
    Image getImage();
    void putImage(Image img);
}
