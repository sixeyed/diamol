package iotd.configuration;

import org.springframework.context.annotation.Bean;

import iotd.services.CacheService;
import iotd.services.MemoryCacheService;

public class BeanConfiguration {

	@Bean
	public CacheService cacheService() {
		return new MemoryCacheService();
	}	
}
