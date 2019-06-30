package iotd;

import org.springframework.context.annotation.Bean;

public class BeanConfiguration {

	@Bean
	public CacheService cacheService() {
		return new MemoryCacheService();
	}	
}
