package iotd;

import java.util.ArrayList;

public interface CacheService {
    Image getImage();
    void putImage(Image img);
}
