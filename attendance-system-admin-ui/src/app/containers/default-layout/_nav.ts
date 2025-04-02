import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Trang chủ',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
  {
    name: 'Danh mục',
    url: '/content',
    iconComponent: { name: 'cil-puzzle' },
    children: [
      {
        name: 'Lớp học',
        url: '/content/classes'
      },
      {
        name: 'Thiết bị',
        url: '/content/devices'
      }
    ]
  },
  {
    name: 'Hệ thống',
    url: '/system',
    iconComponent: { name: 'cil-notes' },
    children: [
      {
        name: 'Quyền',
        url: '/system/role'
      },
      {
        name: 'Người dùng',
        url: '/system/user'
      }
    ]
  },
];
