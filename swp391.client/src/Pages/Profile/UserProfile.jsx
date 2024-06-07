import UserSidebar from "../../Component/UserSidebar";
import MainLayout from "../../Layouts/MainLayout";
import { useUser } from "../../Context/UserContext";
import { useState } from "react";

function UserProfile() {
    const [user, setUser] = useUser();
    return (
        <>
            <MainLayout>
                <div>
                    {user.id}
                </div>
                <div>
                    {user.role}
                </div>
            </MainLayout>
        </>
    );
}

export default UserProfile;