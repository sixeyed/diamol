package iotd;

import java.util.concurrent.TimeUnit;
import org.ehcache.config.CacheConfiguration;
import org.ehcache.config.builders.CacheConfigurationBuilder;
import org.ehcache.config.builders.CacheManagerBuilder;
import org.ehcache.config.builders.ResourcePoolsBuilder;
import org.ehcache.CacheManager;
import org.ehcache.Cache;
import org.ehcache.expiry.Duration;
import org.ehcache.expiry.Expirations;
import org.springframework.stereotype.Service;

@Service("CacheService")
public class MemoryCacheService implements CacheService { 
    private static Cache<String, Image> _ImageCache;

    public MemoryCacheService() {
        CacheManager cacheManager = CacheManagerBuilder.newCacheManagerBuilder().withCache("ImageCache",
        CacheConfigurationBuilder.newCacheConfigurationBuilder(String.class,Image.class,
            ResourcePoolsBuilder.heap(100))
            .withExpiry(Expirations.timeToLiveExpiration(new Duration(2, TimeUnit.HOURS)))
            .build()).build(true);
        _ImageCache = cacheManager.getCache("ImageCache", String.class, Image.class);
    }

    public Image getImage(){
        return (Image) _ImageCache.get("_Image");
    }      

    public void putImage(Image img){
        _ImageCache.put("_Image", img);
    }
}