package com.civica.hackrhub

import com.civica.hackrhub.auth.HackrhubAuthenticator
import com.civica.hackrhub.auth.HackrhubAuthorizer
import com.civica.hackrhub.auth.UserPrincipal
import com.fasterxml.jackson.module.kotlin.KotlinModule
import com.sfwltd.lineup.util.RestExceptionMapper
import com.sfwltd.lineup.util.URLMappedFilter
import io.dropwizard.Application
import io.dropwizard.Configuration
import io.dropwizard.assets.AssetsBundle
import io.dropwizard.auth.AuthValueFactoryProvider
import io.dropwizard.auth.oauth.OAuthCredentialAuthFilter
import io.dropwizard.lifecycle.ServerLifecycleListener
import io.dropwizard.setup.Bootstrap
import io.dropwizard.setup.Environment
import io.dropwizard.views.ViewBundle
import org.eclipse.jetty.server.Server
import org.eclipse.jetty.servlets.CrossOriginFilter
import org.glassfish.jersey.server.filter.RolesAllowedDynamicFeature
import org.jooq.impl.DSL
import java.text.SimpleDateFormat
import java.util.*
import javax.servlet.DispatcherType

/**
 * Created by SKX on 04/05/2017.
 */
class HackrhubApplication: Application<HackrhubConfig>() {
    override fun getName(): String {
        return "Hackrhub"
    }

    private val rootPackage:String = HackrhubApplication::class.java.`package`.name.substring(0,HackrhubApplication::class.java.`package`.name.lastIndexOf('.'))

    override fun initialize(bootstrap: Bootstrap<HackrhubConfig>) {
        bootstrap.addBundle(ViewBundle<HackrhubConfig>())
        bootstrap.addBundle(AssetsBundle())
    }
    override fun run(configuration: HackrhubConfig, environment: Environment) {
        environment.objectMapper.registerModule(KotlinModule())
        environment.objectMapper.dateFormat= SimpleDateFormat("yyyy-MM-dd");

        environment.jersey().packages(rootPackage)
        val filter = OAuthCredentialAuthFilter.Builder<UserPrincipal>()
                .setAuthenticator(HackrhubAuthenticator())
                .setAuthorizer(HackrhubAuthorizer())
                .setPrefix("Bearer")
                .buildAuthFilter()
        environment.jersey().register(URLMappedFilter(filter,"^app\\/.*$"))
        environment.jersey().register(RolesAllowedDynamicFeature::class.java)
        environment.jersey().register(RestExceptionMapper::class.java)
        environment.jersey().register(AuthValueFactoryProvider.Binder(UserPrincipal::class.java))
        // Enable CORS headers
        val cors = environment.servlets().addFilter("CORS", CrossOriginFilter::class.java)

        // Configure CORS parameters
        cors.setInitParameter("allowedOrigins", "*")
        cors.setInitParameter("allowedHeaders", "*")
        cors.setInitParameter("allowedMethods", "OPTIONS,GET,PUT,POST,DELETE,HEAD")

        // Add URL mapping
        cors.addMappingForUrlPatterns(EnumSet.allOf(DispatcherType::class.java), true, "/*")

        environment.lifecycle().addServerLifecycleListener(object : ServerLifecycleListener {
            override fun serverStarted(server: Server) {
               //startup stuff
            }
        })
    }

}

class HackrhubConfig : Configuration() {

}
