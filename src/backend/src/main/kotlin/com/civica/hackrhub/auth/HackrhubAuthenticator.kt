package com.civica.hackrhub.auth

import io.dropwizard.auth.Authenticator
import java.util.*

/**
 * Created by SKX on 04/05/2017.
 */
class HackrhubAuthenticator : Authenticator<String, UserPrincipal> {
    override fun authenticate(credentials: String?): Optional<UserPrincipal> {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }
}