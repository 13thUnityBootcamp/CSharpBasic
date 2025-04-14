namespace RaycastSample
{
    internal class Program
    {
        static void Main(string[] args) {
            // layers
            // 1 = ground
            // 2 = enemy
            // 3 = player
            // 4 = interactable

            // bit flags : 필요한 비트들만 올려서 검출하기 위한 값을 할달할 때 사용
            int layerFlags = 1 << 2 | 1 << 4; // interactable enemy , 얼굴

            // 총을 쐈을 때 충돌할 수 있는 대상을 검출하기위한 검출기
            // 이런것을 Bit Flags에서 검출할 때 사용하는 것이 Bit Mask
            // 예시로, 총알이 충돌할 수 있는 대상을 ground, enemy로 정했다면, 충돌 bit mask는 :
            int collisionmask = 1 << 1 | 1 << 2; // 충돌을 검출하기 위한 bit mask, 가면

            if ((layerFlags & collisionmask) > 0) { // 충돌 대상이 맞음
                                                                                                                                                                                                                               
            }
            else { // 충돌할 수 없는 대상

            }

                float rayOriginX = 2, rayOriginY = 3.1f; // 선을 쏘는 시작점 X, Y
            float rayDirectionX = 1f, rayDirectionY = 0f; // 선을 쏘는 방향 X, Y

            float circleColliderCenterX = 10.2f, circleColliderCenterY = 5.2f; // 원충돌제 중심점 X, Y
            float circleColliderRadius = 4f; // 원충돌제 반지름

            //ray 위의 점 x, y에 대한 방정식
            // x = rayOriginX + rayDirectionX + t
            // y = rayOriginY + rayDirectionY + t

            // 원의 방정식
            // (x - circleColliderCenterX) ^ 2 + (y - circleColliderCenterY) ^ 2 = circleColliderRadius^2

            // 교차점 연립방정식
            // (rayOriginX + rayDirectionX * t - circleColliderCenterX) ^ 2 + (rayOriginY + rayDirectionY * t - circleColliderCenterY) ^ 2 = circleColliderRadius ^ 2
            // rayOriginY ^ 2 + rayDirectionY ^ 2 * t ^ 2 + circleColliderCenterX ^ 2 + 2 * rayOriginX * rayDirectionX * t - 2 * rayDirectionX * circleColliderCenterX - 2 * rayOriginX * circleColliderCenter


            // 교츠 여부 판단을 위한 판별식 계산
            float a = rayDirectionX * rayDirectionX + rayDirectionY * rayDirectionY;
            float b = 2 * (rayDirectionX * (rayOriginX - circleColliderCenterX) + rayDirectionY * (rayOriginY - circleColliderCenterY));
            float c = (rayOriginX - circleColliderCenterX) * (rayOriginX - circleColliderCenterX) +
                (rayOriginY - circleColliderCenterY) * (rayOriginY - circleColliderCenterY) -
                circleColliderRadius * circleColliderRadius;

            float discriminant = b * b - 4 * a * c;

            // 조건문
            // if의 조건이 참인 경우에만 구현부 실행
            //if (조건1) {
            //     조건1이 참일 때 실행할 내용
            //}
            //else if(조건2) {
            //     조건1이 거짓이고 조건 2가 참일 때 실행할 내용
            //}
            //else { 
            //     상위 조건들이 모두 거짓일 때 실행할 내용
            //}

            if(discriminant < 0)
                Console.WriteLine("Miss");
            //else { 
            //    float sqrt0fDiscriminant = (float)Math.Sqrt(discriminant);
            //    float t1 =(-b + sqrt0fDiscriminant) / (2f * a);
            //    float t2 = (-b -  sqrt0fDiscriminant) / (2f * a);

            //    if(t1 >=0 && t2 >= 0) {
            //        T = Math.Min(t1, t2);
            //    }
            //    else if {

            //    }
            //}

            
        }
    }
}
